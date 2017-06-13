using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Access;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation
{
    public interface IGameTeamsGenerator
    {
        Task<IEnumerable<GameTeamCreate>> GenerateTeams(int gameId, IEnumerable<GameTeamCreate> teams);
        Task<IEnumerable<GameTeamCreate>> GenerateTeamsBasedOnGame(int newGameId, int baseGameId);
    }

    public class GameTeamsGenerator : IGameTeamsGenerator
    {
        private readonly IGameTeamRepository gameTeamRepository;
        public GameTeamsGenerator(IGameTeamRepository gameTeamRepository)
        {
            this.gameTeamRepository = gameTeamRepository;
        }

        public async Task<IEnumerable<GameTeamCreate>> GenerateTeamsBasedOnGame(int newGameId, int baseGameId)
        {
            var teams = await gameTeamRepository.GetGameTeams(baseGameId);

            var count = teams.Count();

            foreach(var team in teams)
            {
                if (team.Order == count)
                    team.Order = 1;
                else
                    team.Order += 1;

                team.GameId = newGameId;
            }
            SetFieldCount(teams);

            return teams.ToList();
        }

        private void SetFieldCount(IEnumerable<GameTeamCreate> teams)
        {
            var switchValues = teams.Select(x => x.FieldCount).Distinct().Count() != 1;
            if (switchValues)
            {
                var maxFieldCount = teams.Max(x => x.FieldCount);
                var minFieldCount = teams.Min(x => x.FieldCount);
                foreach(var team in teams)
                {
                    if (team.FieldCount == maxFieldCount)
                        team.FieldCount = minFieldCount;
                    else
                        team.FieldCount += 1;
                }
            }
        }

        public async Task<IEnumerable<GameTeamCreate>> GenerateTeams(int gameId, IEnumerable<GameTeamCreate> teams)
        {
            int order = 1;
            teams.ForEach(x =>
            {
                x.GameId = gameId;
                x.Order = order;
                order++;
            });

            return teams;
        }
    }
}
