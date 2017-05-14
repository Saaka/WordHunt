using Dapper;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Data.Entities;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Repository
{
    public interface IGameStatusRepository
    {
        Task CreateInitialGameStatus(int gameId, int firstTeamId);
        Task<Models.Games.Access.LatestStatus> UpdateCurrentStatus(int gameId, int teamId, Status gameStatus);
    }

    public class GameStatusRepository : IGameStatusRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;

        public GameStatusRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
        }

        public async Task<Models.Games.Access.LatestStatus> UpdateCurrentStatus(int gameId, int teamId, Status gameStatus)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstAsync<Models.Games.Access.LatestStatus>(CreationQueries.CreateAndUpdateGameStatus, new { GameId = gameId, CurrentTeamId = teamId, Status = (int)gameStatus });
            }
        }

        public async Task CreateInitialGameStatus(int gameId, int firstTeamId)
        {
            var entity = CreateStatus(gameId, firstTeamId);

            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreationQueries.CreateGameStatusQuery, entity);
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
