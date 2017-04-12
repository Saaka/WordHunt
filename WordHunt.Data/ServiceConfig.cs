using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Services.Words;
using WordHunt.DataInterfaces.Words;

namespace WordHunt.Data
{
    public static class ServiceConfig
    {
        //Configure services from WordHunt.Data library.
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<IWordProvider, WordProvider>();
            services.AddScoped<IWordProviderValidator, WordProviderValidator>();

            return services;
        }
    }
}
