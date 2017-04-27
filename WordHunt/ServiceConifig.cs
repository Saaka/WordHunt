﻿using Microsoft.Extensions.DependencyInjection;
using WordHunt.Games.Create;
using WordHunt.Games.Create.Repository;
using WordHunt.Services.Categories;
using WordHunt.Services.Categories.Mapper;
using WordHunt.Services.Languages;
using WordHunt.Services.Users;
using WordHunt.Services.Words;
using WordHunt.Services.Words.Mapper;

namespace WordHunt
{
    public static class ServiceConifig
    {
        //Configure services from WordHunt library.
        public static IServiceCollection ConfigureWordHuntServices(this IServiceCollection services)
        {            
            services.AddScoped<IGameCreator, GameCreator>();
            services.AddScoped<IGameCreatorValidator, GameCreatorValidator>();

            services.AddScoped<IUserService, UserService>();

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

            services.AddScoped<ILanguageProvider, LanguageProvider>();

            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
