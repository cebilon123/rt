using System;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Orders.Application.Handlers;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Infrastructure.Cqrs;
using Rc.Services.Orders.Infrastructure.Rabbit;
using Rc.Services.Orders.Infrastructure.Services;

namespace Rc.Services.Orders.Infrastructure.Initialize
{
    public static class Extensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return services;
        }

        public static IServiceCollection AddQueryDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, string connectionStr)
            => services.AddSingleton(typeof(IBus), RabbitHutch.CreateBus(connectionStr));

        public static IServiceCollection AddMessageBroker(this IServiceCollection services)
            => services.AddTransient<IMessageBroker, MessageBroker>();
        
        public static IServiceCollection AddDomainEventsToEventsMapper(this IServiceCollection services)
            => services.AddTransient<IDomainEventToEventMapper, DomainEventToEventMapper>();
        
        public static IServiceCollection AddEventsProcessor(this IServiceCollection services)
            => services.AddTransient<IEventProcessor, EventProcessor>();
    }
}