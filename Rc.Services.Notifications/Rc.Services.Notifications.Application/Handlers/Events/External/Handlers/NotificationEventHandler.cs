using System.Threading.Tasks;

namespace Api.Application.Handlers.Events.External.Handlers
{
    public class NotificationEventHandler : IEventHandler<Notification>
    {
        public Task OnEvent(Notification @event)
        {
            throw new System.NotImplementedException();
        }
    }
}