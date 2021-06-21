using Api.Application.Services;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Rc.Services.Notifications.Infrastructure.Services
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IBus _bus;
        private readonly IServiceScopeFactory _scopeFactory;

        public MessageBroker(IBus bus, IServiceScopeFactory scopeFactory)
        {
            _bus = bus;
            _scopeFactory = scopeFactory;
        }
    }
}