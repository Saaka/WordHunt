using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Services.Words;
using WordHunt.Data.Services.Words.Mapper;
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
            services.AddScoped<IWordCreator, WordCreator>();
            services.AddScoped<IWordCreatorValidator, WordCreatorValidator>();
            services.AddScoped<IWordMapper, WordMapper>();
            services.AddScoped<IWordUpdater, WordUpdater>();
            services.AddScoped<IWordUpdaterValidator, WordUpdaterValidator>();

            return services;
        }
    }
}
