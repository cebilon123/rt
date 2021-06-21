using System;
using EasyNetQ;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Fraud.Application.Handlers;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Cqrs;
using Rc.Services.Fraud.Infrastructure.Rabbit;
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
        
        public static IServiceCollection AddMessageBroker(this IServiceCollection services)
            => services.AddTransient<IMessageBroker, MessageBroker>();

        public static IServiceCollection AddQueryDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return services;
        }
        
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, string connectionStr)
            => services.AddSingleton(typeof(IBus), RabbitHutch.CreateBus(connectionStr));

        public static IServiceCollection AddHangfire(this IServiceCollection services, string mongoConnectionString)
        {
            services.AddHangfire(c => c.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage($"{mongoConnectionString}/rc_services_fraud", new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new DropMongoMigrationStrategy(),
                        BackupStrategy = new CollectionMongoBackupStrategy()
                    }
                }));

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseDashboardHangfire(this IApplicationBuilder app)
            => app.UseHangfireDashboard();
    }
}