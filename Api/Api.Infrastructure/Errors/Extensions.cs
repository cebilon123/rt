﻿using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Errors
{
    public static class Extensions
    {
        public static IServiceCollection AddExceptionToErrorMapper<TImplementation>(this IServiceCollection services)
            where TImplementation : class, IExceptionToResponseMapper
        {
            return services.AddSingleton<IExceptionToResponseMapper, TImplementation>();
        }
    }
}