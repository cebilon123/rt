using Rc.Services.Fraud.Application.Events.External;

namespace Rc.Services.Fraud.Application.Events
{
    public interface IEvent
    {
        bool SendNotification { get; }
        Notification GetNotification();
    }
}