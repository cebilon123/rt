namespace Rc.Services.Fraud.Application.Events.External
{
    public class OrderAccepted : IEvent
    {
        public bool SendNotification => true;

        public Notification GetNotification()
            => new(NotificationType.Success, "Order was accepted");
    }
}