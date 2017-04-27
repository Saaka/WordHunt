using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Data.Entities;
using WordHunt.Games.Mappings;

namespace WordHunt.Games.Repository
{
    public interface IGameStatusRepository
    {
        Task CreateInitialGameStatus(int gameId, int firstTeamId);
    }

    public class GameStatusRepository : IGameStatusRepository
    {
        private const string CreateGameStatusQuery = @"INSERT INTO GameStatuses ([CurrentTeamId], [GameId], [Latest], [Status])
                                    VALUES (@CurrentTeamId, @GameId, @Latest, @Status)";
        
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IGameMapper gameMapper;

        public GameStatusRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IGameMapper gameMapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.gameMapper = gameMapper;
        }
        public async Task CreateInitialGameStatus(int gameId, int firstTeamId)
        {
            var entity = gameMapper.MapStatus(gameId, firstTeamId);

            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreateGameStatusQuery, entity);
            }
        }
    }
}
