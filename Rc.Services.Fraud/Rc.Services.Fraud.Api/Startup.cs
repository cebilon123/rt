using System;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rc.Services.Fraud.Api.Helpers.Swagger;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Errors;
using Rc.Services.Fraud.Infrastructure.Initialize;
using Rc.Services.Fraud.Infrastructure.Repositories;
using Rc.Services.Fraud.Infrastructure.Services;

namespace Rc.Services.Fraud.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            BaseConfiguration(services);
        }

        private void BaseConfiguration(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFile("Logs/app_{0:yyyy}-{0:MM}-{0:dd}.log",
                    fileLoggerOpts =>
                    {
                        fileLoggerOpts.FormatLogFileName = fName => string.Format(fName, DateTime.UtcNow);
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
                c.OperationFilter<ApplySwaggerDescriptionFilter>();
            });
            services
                .AddHttpContextAccessor()
                .AddCommandDispatcher()
                .AddQueryDispatcher()
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddExceptionToErrorMapper<ExceptionToResponseMapper>()
                .AddRabbitMq(Configuration["RabbitMq"])
                .AddMongoDb(Configuration["DatabaseConnectionString"])
                .AddTransient<IOrdersApi, OrdersApi>()
                .AddMessageBroker()
                .RegisterAntiFraudRules()
                .RegisterAntiFraudOrderValidator()
                .RegisterAntiFraudService()
                .AddHangfire(Configuration["DatabaseConnectionString"]);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:8080")
                    .AllowCredentials();
            });

            app.UseDashboardHangfire();
            
            RecurringJob.AddOrUpdate(() => app.ApplicationServices.GetService<IAntiFraudService>().ValidateNewOrders(), "* * * * *" );
            
            app.UseHttpsRedirection();

            app.ApplicationServices.GetService(typeof(IAntiFraudService));

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}