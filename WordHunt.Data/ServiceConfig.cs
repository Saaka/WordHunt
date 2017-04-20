using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.Data.Identity;
using WordHunt.Data.Initializer;
using WordHunt.Data.Services.Categories;
using WordHunt.Data.Services.Categories.Mapper;
using WordHunt.Data.Services.Languages;
using WordHunt.Data.Services.Words;
using WordHunt.Data.Services.Words.Mapper;
using WordHunt.DataInterfaces.Categories;
using WordHunt.DataInterfaces.Languages;
using WordHunt.DataInterfaces.Words;

namespace WordHunt.Data
{
    public static class ServiceConfig
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(opt => opt.Cookies.ApplicationCookie.AutomaticChallenge = false)
                .AddEntityFrameworkStores<AppDbContext>();

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
            services.AddScoped<IWordProvider, WordProvider>();
            services.AddScoped<IWordProviderValidator, WordProviderValidator>();
            services.AddScoped<IWordCreator, WordCreator>();
            services.AddScoped<IWordCreatorValidator, WordCreatorValidator>();
            services.AddScoped<IWordMapper, WordMapper>();
            services.AddScoped<IWordUpdater, WordUpdater>();
            services.AddScoped<IWordUpdaterValidator, WordUpdaterValidator>();

            services.AddScoped<ICategoryProvider, CategoryProvider>();
            services.AddScoped<ICategoryProviderValidator, CategoryProviderValidator>();
            services.AddScoped<ICategoryCreator, CategoryCreator>();
            services.AddScoped<ICategoryCreatorValidator, CategoryCreatorValidator>();
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<ICategoryUpdater, CategoryUpdater>();
            services.AddScoped<ICategoryUpdaterValidator, CategoryUpdaterValidator>();

            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IDBInitializer, DBInitializer>();

            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();

            services.AddScoped<ILanguageProvider, LanguageProvider>();

            return services;
        }
    }
}
