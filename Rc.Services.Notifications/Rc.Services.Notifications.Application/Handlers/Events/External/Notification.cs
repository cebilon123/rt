namespace Api.Application.Handlers.Events.External
{
    public class Notification : IEvent
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Success
    }
}