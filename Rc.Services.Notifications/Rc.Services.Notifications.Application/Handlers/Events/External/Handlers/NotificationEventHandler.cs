using System.Threading.Tasks;
using Api.Application.Hubs;
using Api.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Api.Application.Handlers.Events.External.Handlers
{
    public class NotificationEventHandler : IEventHandler<Notification>
    {
        private readonly IHubContext<Notifications> _hub;

        public NotificationEventHandler(IHubContext<Notifications> hub)
        {
            _hub = hub;
        }
        
        public async Task OnEvent(Notification @event)
        {
            await _hub.Clients.All.SendAsync("Notification",@event);
        }
    }
}