using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;
using Rc.Services.Fraud.Application.Events;
using Rc.Services.Fraud.Application.Services;

namespace Rc.Services.Fraud.Infrastructure.Rabbit
{
    public class MessageBroker : IMessageBroker
    {
        private const string NotificationQueueName = "Rc.Services.Notifications";
        private const string CurrentExchangeName = "Fraud";

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

                if (@event.SendNotification)
                {
                    await _bus.Advanced.PublishAsync(_exchange, NotificationQueueName,true, new Message<string>(JsonConvert.SerializeObject(@event.GetNotification())));
                }
            }
        }
    }
}