using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Rc.Services.Orders.Application.Events;
using Rc.Services.Orders.Application.Services;

namespace Rc.Services.Orders.Infrastructure.Rabbit
{
    public class MessageBroker : IMessageBroker
    {
        private const string QueueName = "Rc.Services.Orders";
        private readonly IBus _bus;

        public MessageBroker(IBus bus)
        {
            _bus = bus;
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
            }
        }
    }
}