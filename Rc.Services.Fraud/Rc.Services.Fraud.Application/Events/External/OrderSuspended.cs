namespace Rc.Services.Fraud.Application.Events.External
{
    public class OrderSuspended : IEvent
    {
        public bool SendNotification => true;

        public Notification GetNotification()
            => new(NotificationType.Error, "Order was suspended and waits for review");
    }
}