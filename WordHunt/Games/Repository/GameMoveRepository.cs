using Dapper;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;

namespace WordHunt.Games.Repository
{
    public interface IGameMoveRepository
    {
        Task<int> SaveMove(int gameId, MoveType moveType, int teamId, int? fieldId = null);
    }

    class GameMoveRepository : IGameMoveRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;

        public GameMoveRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
        }

        public async Task<int> SaveMove(int gameId, MoveType moveType, int teamId, int? fieldId = null)
        {
            using(var connection = connectionFactory.CreateConnection())
            {
                var currentTime = timeProvider.GetCurrentTime();
                return await connection.ExecuteScalarAsync<int>(CreationQueries.CreateGameMoveQuery, new { GameId = gameId, Type = moveType, TeamId = teamId, FieldId = fieldId, Timestamp = currentTime });
            }
        }
    }
}
