using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Moves.Helpers
{
    public interface INextTeamProvider
    {
        Task<NextTeam> GetNextTeam(int gameId, EndMode endMode);
    }

    public class NextTeamProvider : INextTeamProvider
    {
        private readonly IGameTeamRepository gameTeamRepository;

        public NextTeamProvider(IGameTeamRepository gameTeamRepository)
        {
            this.gameTeamRepository = gameTeamRepository;
        }

        public async Task<NextTeam> GetNextTeam(int gameId, EndMode endMode)
        {
            switch (endMode)
            {
                case EndMode.SuddenDeath:
                    return await gameTeamRepository.GetNextTeam(gameId);

                default:
                    throw new NotImplementedException("Not yet implemented");
            }
        }
    }
}