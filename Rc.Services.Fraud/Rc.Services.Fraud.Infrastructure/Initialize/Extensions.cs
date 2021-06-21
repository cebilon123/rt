using System;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Fraud.Application.Handlers;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Cqrs;
using Rc.Services.Fraud.Infrastructure.Services;
using Rc.Services.Fraud.Infrastructure.Services.AntiFraud;

namespace Rc.Services.Fraud.Infrastructure.Initialize
{
    public static class CqrsExtensions
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

        public static IServiceCollection RegisterAntiFraudRules(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IAntiFraudRule)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection RegisterAntiFraudOrderValidator(this IServiceCollection services)
            => services.AddTransient<IAntiFraudOrderValidator, AntiFraudOrderValidator>();
        
        public static IServiceCollection RegisterAntiFraudService(this IServiceCollection services)
            => services.AddTransient<IAntiFraudService, AntiFraudService>();

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
    }
}