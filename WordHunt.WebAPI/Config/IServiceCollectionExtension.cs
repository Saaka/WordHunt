using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Config;
using WordHunt.Data;
using WordHunt.WebAPI.Auth.Token;

namespace WordHunt.WebAPI.Config
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Register dependencies from the application.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        /// <returns></returns>
        public static IServiceCollection RegisterWebApiDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, WordHuntConfiguration>();
            services.AddScoped<IAuthConfiguration, WordHuntConfiguration>();
            services.AddScoped<ISeedConfiguration, WordHuntConfiguration>();
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
