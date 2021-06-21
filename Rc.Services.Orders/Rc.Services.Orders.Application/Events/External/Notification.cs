namespace Rc.Services.Orders.Application.Events.External
{
    public class Notification
    {
        public NotificationType Type { get; }
        public string Message { get;  }

        public Notification(NotificationType type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Success
    }
}