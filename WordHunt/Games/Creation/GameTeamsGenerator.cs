using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation
{
    public interface IGameTeamsGenerator
    {
        Task<IEnumerable<GameTeamCreate>> GenerateTeams(int gameId, IEnumerable<GameTeamCreate> teams);
    }

    public class GameTeamsGenerator : IGameTeamsGenerator
    {
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
