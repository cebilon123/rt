using System.Threading.Tasks;
using Api.Application.Handlers.Events.External;
using Api.Application.Handlers.Events.External.Handlers;
using Api.Application.Services;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Rc.Services.Notifications.Infrastructure.Services
{
    public class MessageListener : IMessageListener
    {
        private const string QueueName = "Rc.Services.Notifications";
        
        private readonly IBus _bus;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Queue _queue;

        public MessageListener(IBus bus, IServiceScopeFactory scopeFactory)
        {
            _bus = bus;
            _scopeFactory = scopeFactory;
            _queue = _bus.Advanced.QueueDeclare(QueueName);
            
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            using IServiceScope scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IEventHandler<Notification>>();

            _bus.Advanced.Consume<string>(_queue, (message, info) =>
            {
                var notification = JsonConvert.DeserializeObject<Notification>(message.Body);
                service.OnEvent(notification);
            });
        }
    }
}