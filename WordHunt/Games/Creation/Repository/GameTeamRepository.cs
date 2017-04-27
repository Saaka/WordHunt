using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Games.Creation.Mappings;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation.Repository
{
    public interface IGameTeamRepository
    {
        Task CreateGameTeams(IEnumerable<GameTeamCreate> model);
    }

    public class GameTeamRepository : IGameTeamRepository
    {
        private const string CreateGameTeamQuery = @"INSERT INTO GameTeams ([FieldCount], [GameId], [Name], [Order], [UserId], [RemainingFieldCount])
                                    VALUES (@FieldCount, @GameId, @Name, @Order, @UserId, @RemainingFieldCount)";

        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IGameMapper gameMapper;

        public GameTeamRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IGameMapper gameMapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.gameMapper = gameMapper;
        }

        public async Task CreateGameTeams(IEnumerable<GameTeamCreate> models)
        {
            var entities = models.Select(x => gameMapper.MapGameTeam(x));

            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreateGameTeamQuery, entities);
            }
        }
    }
}
