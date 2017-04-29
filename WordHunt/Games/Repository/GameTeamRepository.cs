using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Games.Mappings;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Repository
{
    public interface IGameTeamRepository
    {
        Task<IEnumerable<GameTeamCreated>> CreateGameTeams(IEnumerable<GameTeamCreate> model);
        Task<int> GetFirstTeamId(int gameId);
    }

    public class GameTeamRepository : IGameTeamRepository
    {
        private const string CreateGameTeamQuery = @"INSERT INTO GameTeams ([FieldCount], [GameId], [Name], [Order], [UserId], [RemainingFieldCount])
                                    OUTPUT INSERTED.[Id], INSERTED.[Order], INSERTED.[FieldCount]
                                    VALUES (@FieldCount, @GameId, @Name, @Order, @UserId, @RemainingFieldCount)";

        private const string GetFirstTeamIdQuery = @"SELECT TOP 1 [Id] FROM GameTeams WHERE [GameId] = @GameId ORDER BY [Order]";

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

        public async Task<IEnumerable<GameTeamCreated>> CreateGameTeams(IEnumerable<GameTeamCreate> models)
        {
            var entities = models.Select(x => gameMapper.MapGameTeam(x)).ToArray();

            List<GameTeamCreated> outputList = new List<GameTeamCreated>();
            using (var connection = connectionFactory.CreateConnection())
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    var created = await connection.QueryFirstAsync<GameTeamCreated>(CreateGameTeamQuery, entities[i]);
                    outputList.Add(created);
                }
            }

            return outputList;
        }

        public async Task<int> GetFirstTeamId(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                var firstTeamId = await connection.QueryFirstAsync<int>(GetFirstTeamIdQuery, new { GameId = gameId });

                return firstTeamId;
            }
        }
    }
}
