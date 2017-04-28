using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Games.Mappings;
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
                                    VALUES (@Name, @BoardWidth, @BoardHeight, @TeamCount, @TrapCount, @Type, @EndMode, @UserId, @CreationDate, @LanguageId)
                                    SELECT CAST(scope_identity() as int)";
        
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IGameMapper gameMapper;

        public GameRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IGameMapper gameMapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.gameMapper = gameMapper;
        }

        public async Task<GameCreated> SaveNewGame(GameCreate model)
        {
            var entity = gameMapper.MapGame(model);
            entity.CreationDate = timeProvider.GetCurrentTime();

            using (var connection = connectionFactory.CreateConnection())
            {
                var game = await connection.QueryFirstAsync<GameCreated>(CreateGameQuery, entity);

                return game;
            }
        }
    }
}
