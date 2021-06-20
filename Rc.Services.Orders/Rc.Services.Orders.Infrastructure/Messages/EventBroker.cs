using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Orders.Application.Messages;

namespace Rc.Services.Orders.Infrastructure.Messages
{
    public class EventBroker : IEventBroker
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public EventBroker(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            using IServiceScope scope = _serviceFactory.CreateScope();
            var eventHandlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers) await eventHandler.HandleAsync(@event);
        }
    }
}