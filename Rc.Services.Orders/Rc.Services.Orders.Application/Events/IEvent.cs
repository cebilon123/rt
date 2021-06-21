using Rc.Services.Orders.Application.Events.External;

namespace Rc.Services.Orders.Application.Events
{
    public interface IEvent
    {
        bool SendNotification { get; }
        Notification GetNotification();
    }
}