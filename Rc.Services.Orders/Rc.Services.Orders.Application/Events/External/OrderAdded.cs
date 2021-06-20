using System;

namespace Rc.Services.Orders.Application.Events.External
{
    public class OrderAdded : IEvent
    {
        public Guid OrderId { get; }

        public OrderAdded(Guid orderId)
            => OrderId = orderId;
    }
}