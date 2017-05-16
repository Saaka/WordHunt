using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WordHunt.Config;
using WordHunt.Config.Auth;
using WordHunt.Data;
using WordHunt.Data.Initializer;
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
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddSingleton<Games.Broadcaster.IEventBroadcaster, Hubs.EventBroadcaster>();

            services.AddTransient<ITokenUserContextProvider, TokenUserContextProvider>();
            services.AddTransient<System.Security.Claims.ClaimsPrincipal>(
                s => s.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>().HttpContext.User);

            return services;
        }

        /// <summary>
        /// Register policies for the system to authorize requests.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SystemPolicies.AdminOnly, policy =>
                {
                    policy.RequireClaim(SystemClaims.IsAdmin, "true");
                });
            });

            return services;
        }
    }
}
