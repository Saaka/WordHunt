using Microsoft.Extensions.DependencyInjection;
using WordHunt.Base.Services;

namespace WordHunt.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureBaseServices(this IServiceCollection services)
        {
            services.AddScoped<ITimeProvider, TimeProvider>();

            return services;
        }
    }
}
