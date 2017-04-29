using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Data.Entities;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Repository
{
    public interface IGameRepository
    {
        Task<GameCreated> SaveNewGame(GameCreate game);
    }

    public class GameRepository : IGameRepository
    {
        private const string CreateGameQuery = @"INSERT INTO GAMES (Name, BoardWidth, BoardHeight, TeamCount, TrapCount, Type, EndMode, UserId, CreationDate, LanguageId)
                                    OUTPUT INSERTED.[Id], INSERTED.[BoardWidth], INSERTED.[BoardHeight], INSERTED.[TrapCount], INSERTED.[LanguageId]
                                    VALUES (@Name, @BoardWidth, @BoardHeight, @TeamCount, @TrapCount, @Type, @EndMode, @UserId, @CreationDate, @LanguageId)";

        
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IMapper mapper;

        public GameRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IMapper mapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.mapper = mapper;
        }

        public async Task<GameCreated> SaveNewGame(GameCreate model)
        {
            var entity = mapper.Map<Game>(model);
            entity.CreationDate = timeProvider.GetCurrentTime();

            using (var connection = connectionFactory.CreateConnection())
            {
                var game = await connection.QueryFirstAsync<GameCreated>(CreateGameQuery, entity);

                return game;
            }
        }
    }
}
