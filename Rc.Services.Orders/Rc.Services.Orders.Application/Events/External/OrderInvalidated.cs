namespace Rc.Services.Orders.Application.Events.External
{
    public class OrderInvalidated : IEvent
    {
        public bool SendNotification => true;

        public Notification GetNotification()
            => new Notification(NotificationType.Warning, "Order data was not valid");
    }
}