using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Data.Entities;

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

        public GameStatusRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
        }

        public async Task CreateInitialGameStatus(int gameId, int firstTeamId)
        {
            var entity = CreateStatus(gameId, firstTeamId);

            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreateGameStatusQuery, entity);
            }
        }

        public GameStatus CreateStatus(int gameId, int firstTeamId)
        {
            return new GameStatus()
            {
                CurrentTeamId = firstTeamId,
                GameId = gameId,
                Latest = true,
                Status = Base.Enums.Game.Status.Created
            };
        }
    }
}
