using System;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Orders.Application.Messages;
using Rc.Services.Orders.Infrastructure.Messages;

namespace Rc.Services.Orders.Infrastructure.Initialize
{
    public static class MessagesExtensions
    {
        public static IServiceCollection AddEventBroker(this IServiceCollection services)
        {
            services.AddSingleton<IEventBroker, EventBroker>();
            return services;
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }
    }
}