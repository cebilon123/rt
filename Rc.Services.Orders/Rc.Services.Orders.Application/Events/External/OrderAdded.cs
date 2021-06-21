using System;

namespace Rc.Services.Orders.Application.Events.External
{
    public class OrderAdded : IEvent
    {
        public Guid OrderId { get; }

        public OrderAdded(Guid orderId)
            => OrderId = orderId;

        public bool SendNotification => true;

        public Notification GetNotification()
            => new (NotificationType.Success, "Order accepted and waits for fraud check");
    }
}