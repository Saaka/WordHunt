﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WordHunt.Games.Creation;
using WordHunt.Games.Moves;
using WordHunt.Games.Moves.Helpers;
using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;
using WordHunt.Mappings;
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
        public static IServiceCollection ConfigureMappings(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(new[]
                {
                    typeof(WordHuntMapperProfile)
                });
            });

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(s => new Mapper(s.GetRequiredService<IConfigurationProvider>(), s.GetService));

            return services;
        }

        //Configure services from WordHunt library.
        public static IServiceCollection ConfigureWordHuntServices(this IServiceCollection services)
        {
            //Game move
            services.AddScoped<IMoveValidatorFactory, MoveValidatorFactory>();
            services.AddScoped<IGameMoveManager, GameMoveManager>();
            services.AddScoped<IGameMoveRepository, GameMoveRepository>();
            
            //Game access

            //GAME CREATION related classes
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameTeamRepository, GameTeamRepository>();
            services.AddScoped<IGameStatusRepository, GameStatusRepository>();
            services.AddScoped<IGameFieldRepository, GameFieldRepository>();
            
            services.AddScoped<IGameCreator, GameCreator>();
            services.AddScoped<IGameCreatorValidator, GameCreatorValidator>();

            services.AddScoped<IGameFieldsGenerator, GameFieldsGenerator>();
            services.AddScoped<IGameTeamsGenerator, GameTeamsGenerator>();

            services.AddScoped<IRandomWordRepository, RandomWordRepository>();

            services.AddScoped<INextTeamProvider, NextTeamProvider>();
            services.AddScoped<IEndGameChecker, EndGameChecker>();

            //User related classes
            services.AddScoped<IUserService, UserService>();

            //Word related classes (categories, languages etc)
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
            
            return services;
        }
    }
}
