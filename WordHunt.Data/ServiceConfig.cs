using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.Data.Identity;
using WordHunt.Data.Initializer;
using WordHunt.DataInterfaces.Categories;
using WordHunt.DataInterfaces.Languages;
using WordHunt.DataInterfaces.Users;
using WordHunt.DataInterfaces.Words;

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

            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppUserClaimsProvider, AppUserManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();            

            return services;
        }
    }
}
