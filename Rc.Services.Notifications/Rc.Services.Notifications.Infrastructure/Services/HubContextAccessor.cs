using Api.Application.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Rc.Services.Notifications.Infrastructure.Services
{
    public class HubContextAccessor : IHubContextAccessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public HubContextAccessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            using var scope = _scopeFactory.CreateScope();
            NotificationHub = (IHubContext<Hub>)scope.ServiceProvider.GetService(typeof(IHubContext<Hub>));
            var temp = NotificationHub;
        }
        
        public IHubContext<Hub> NotificationHub { get; }
    }
}