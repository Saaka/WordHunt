using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Models.Games.Creation;
using AutoMapper;
using WordHunt.Data.Entities;

namespace WordHunt.Games.Repository
{
    public interface IGameTeamRepository
    {
        Task<IEnumerable<GameTeamCreated>> CreateGameTeams(IEnumerable<GameTeamCreate> model);
        Task<int> GetFirstTeamId(int gameId);
        Task<Models.Games.Access.NextTeam> GetNextTeam(int gameId);
    }

    class GameTeamRepository : IGameTeamRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IMapper mapper;

        public GameTeamRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IMapper mapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameTeamCreated>> CreateGameTeams(IEnumerable<GameTeamCreate> models)
        {
            var entities = models.Select(x => mapper.Map<GameTeam>(x)).ToArray();

            List<GameTeamCreated> outputList = new List<GameTeamCreated>();
            using (var connection = connectionFactory.CreateConnection())
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    var created = await connection.QueryFirstAsync<GameTeamCreated>(CreationQueries.CreateGameTeamQuery, entities[i]);
                    outputList.Add(created);
                }
            }

            return outputList;
        }

        public async Task<int> GetFirstTeamId(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                var firstTeamId = await connection.QueryFirstAsync<int>(AccessQueries.GetFirstTeamIdQuery, new { GameId = gameId });

                return firstTeamId;
            }
        }

        public async Task<Models.Games.Access.NextTeam> GetNextTeam(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstAsync<Models.Games.Access.NextTeam>(AccessQueries.GetNextTeamQuery, new { GameId = gameId });
            }
        }
    }
}
