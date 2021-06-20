using System;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rc.Services.Orders.Api.Helpers.Swagger;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Repositories;
using Rc.Services.Orders.Infrastructure.Errors;
using Rc.Services.Orders.Infrastructure.Initialize;
using Rc.Services.Orders.Infrastructure.Rabbit;
using Rc.Services.Orders.Infrastructure.Repositories;
using Rc.Services.Orders.Infrastructure.Repositories.Documents;

namespace Rc.Services.Orders.Api
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
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFile("Logs/app_{0:yyyy}-{0:MM}-{0:dd}.log",
                    fileLoggerOpts =>
                    {
                        fileLoggerOpts.FormatLogFileName = fName => string.Format(fName, DateTime.UtcNow);
                    });
            });
            
            services.AddTransient<IOrderRepository, OrderRepository>();

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
                .AddMongoDb(Configuration["DatabaseConnectionString"])
                .AddRepository<OrderDocument, Guid>("orders")
                .AddRabbitMq(Configuration["RabbitMq"])
                .AddMessageBroker();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}