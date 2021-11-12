using System;
using Api.Core.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Auth
{
    public static class Extensions
    {
        /// <summary>
        /// Register password service singleton in application. Injected through DI because there can be different implementations
        /// for development and release environment.
        /// </summary>
        /// <typeparam name="TPasswordService"> Password service singleton implementation</typeparam>
        /// <returns></returns>
        public static IServiceCollection AddPasswordService<TPasswordService>(this IServiceCollection services)
            where TPasswordService : class, IPasswordService
        {
            return services.AddSingleton<IPasswordService, TPasswordService>();
        }

        public static IServiceCollection AddJwtService<TJwtService>(this IServiceCollection services)
            where TJwtService : class, IJwtService
        {
            return services.AddScoped<IJwtService, TJwtService>();
        }

        public static IServiceCollection AddJwyKeysProvider<TJwtKeysProvider>(this IServiceCollection services,
            TJwtKeysProvider provider)
            where TJwtKeysProvider : class, IJwtKeysProvider
        {
            return services.AddSingleton(typeof(IJwtKeysProvider), provider);
        }

        public static IServiceCollection AddRequestInfoProvider<TRequestInfoProvider>(this IServiceCollection services)
            where TRequestInfoProvider : class, IRequestInfoProvider
        {
            return services.AddScoped(typeof(IRequestInfoProvider), typeof(TRequestInfoProvider));
        }
    }
}