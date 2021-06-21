using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;
using Rc.Services.Orders.Application.Events;
using Rc.Services.Orders.Application.Events.External;
using Rc.Services.Orders.Application.Services;

namespace Rc.Services.Orders.Infrastructure.Rabbit
{
    public class MessageBroker : IMessageBroker
    {
        private const string NotificationQueueName = "Rc.Services.Notifications";
        private const string OrdersQueueName = "Rc.Services.Orders";
        private const string CurrentExchangeName = "Orders";

        private readonly IBus _bus;
        private readonly Exchange _exchange;
        private readonly Queue _notificationQueue;

        public MessageBroker(IBus bus)
        {
            _bus = bus;
            _exchange = _bus.Advanced.ExchangeDeclare(CurrentExchangeName, c =>
            {
                c.WithType(ExchangeType.Fanout);
            });

            _notificationQueue = _bus.Advanced.QueueDeclare(NotificationQueueName);

            _bus.Advanced.Bind(_exchange, _notificationQueue, "A.*");
        }

        public async Task PublishAsync(params IEvent[] events)
            => await PublishAsync(events.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
                return;

            foreach (var @event in events)
            {
                if (@event is null)
                    continue;

                await _bus.SendReceive.SendAsync(OrdersQueueName, @event);

                if (@event.SendNotification)
                {
                    await _bus.Advanced.PublishAsync(_exchange, NotificationQueueName,true, new Message<string>(JsonConvert.SerializeObject(@event.GetNotification())));
                }
            }
        }
    }
}