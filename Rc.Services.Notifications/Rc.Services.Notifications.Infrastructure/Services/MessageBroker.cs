using Api.Application.Handlers.Events.External;
using Api.Application.Handlers.Events.External.Handlers;
using Api.Application.Services;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Rc.Services.Notifications.Infrastructure.Services
{
    public class MessageBroker : IMessageBroker
    {
        private const string QueueName = "Rc.Services.Notifications";
        
        private readonly IBus _bus;
        private readonly IServiceScopeFactory _scopeFactory;

        public MessageBroker(IBus bus, IServiceScopeFactory scopeFactory)
        {
            _bus = bus;
            _scopeFactory = scopeFactory;
            
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            using IServiceScope scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<NotificationEventHandler>();

            _bus.SendReceive.Receive<Notification>(QueueName, n => service.OnEvent(n));
        }
    }
}