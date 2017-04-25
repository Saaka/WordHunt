using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Games;
using WordHunt.Games.Create;

namespace WordHunt
{
    public static class ServiceConifig
    {
        //Configure services from WordHunt library.
        public static IServiceCollection ConfigureWordHuntServices(this IServiceCollection services)
        {
            services.AddScoped<IGameCreator, GameCreator>();

            return services;
        }
    }
}
