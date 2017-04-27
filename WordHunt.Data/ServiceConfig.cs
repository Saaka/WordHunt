using Microsoft.Extensions.DependencyInjection;
using WordHunt.Data.Connection;
using WordHunt.Data.Entities;
using WordHunt.Data.Identity;
using WordHunt.Data.Initializer;

namespace WordHunt.Data
{
    public static class ServiceConfig
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(opt => opt.Cookies.ApplicationCookie.AutomaticChallenge = false)
                .AddEntityFrameworkStores<AppDbContext, int>();

            return services;
        }

        public static IServiceCollection RegisterContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

            return services;
        }

        //Configure services from WordHunt.Data library.
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IAppDbInitializerContext, AppDbContext>();
            services.AddScoped<IDBInitializer, DBInitializer>();

            services.AddScoped<IIdentityUserManager, IdentityUserManager>();
            services.AddScoped<IIdentityUserClaimsProvider, IdentityUserManager>();
            services.AddScoped<IIdentityRoleManager, IdentityRoleManager>();
            
            services.AddScoped<IDbTransactionFactory, DbTransactionFactory>();
            services.AddScoped<IDbConnectionProvider, DbConnectionProvider>();

            return services;
        }
    }
}
