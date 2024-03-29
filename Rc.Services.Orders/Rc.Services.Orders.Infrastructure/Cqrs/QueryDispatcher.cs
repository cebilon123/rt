﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Orders.Application.Handlers;

#pragma warning disable 8600
namespace Rc.Services.Orders.Infrastructure.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public QueryDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            TResult result;
            using (IServiceScope scope = _serviceFactory.CreateScope())
            {
                Type serviceType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                object requiredService = scope.ServiceProvider.GetRequiredService(serviceType);
                MethodInfo method = serviceType.GetMethod("HandleAsync");

                object obj;
                if ((object) method == null)
                    obj = (object) null;
                else
                    obj = method.Invoke(requiredService, new object[1]
                    {
                        (object) query
                    });
                result = await (Task<TResult>) obj!;
            }

            return result;
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
        {
            TResult result;
            using (IServiceScope scope = _serviceFactory.CreateScope())
            {
                result = await scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>()
                    .HandleAsync(query);
            }

            return result;
        }
    }
}
#pragma warning restore 8600