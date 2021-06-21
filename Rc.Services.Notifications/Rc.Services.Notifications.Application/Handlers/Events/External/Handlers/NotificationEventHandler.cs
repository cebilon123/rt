using System.Threading.Tasks;
using Api.Application.Services;

namespace Api.Application.Handlers.Events.External.Handlers
{
    public class NotificationEventHandler : IEventHandler<Notification>
    {
        private readonly IHubContextAccessor _accessor;

        public NotificationEventHandler(IHubContextAccessor accessor)
        {
            _accessor = accessor;
        }
        
        public Task OnEvent(Notification @event)
        {
            throw new System.NotImplementedException();
        }
    }
}