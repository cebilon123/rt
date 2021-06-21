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
        private const string QueueName = "Rc.Services.Orders";
        private const string NotificationsQueueName = "Rc.Services.Notifications";
        private const string NotificationsExchangeName = "Notifications";

        private readonly IBus _bus;
        private readonly Exchange _exchange;
        private readonly Queue _queue;

        public MessageBroker(IBus bus)
        {
            _bus = bus;
            _exchange = _bus.Advanced.ExchangeDeclare(NotificationsExchangeName, c =>
            {
                c.WithType(ExchangeType.Fanout);
            });

            _queue = _bus.Advanced.QueueDeclare(NotificationsQueueName);

            _bus.Advanced.Bind(_exchange, _queue, "A.*");
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

                await _bus.SendReceive.SendAsync(QueueName, @event);

                if (@event.SendNotification)
                {
                    await _bus.Advanced.PublishAsync(_exchange, NotificationsQueueName,true, new Message<string>(JsonConvert.SerializeObject(@event.GetNotification())));
                }
            }
        }
    }
}