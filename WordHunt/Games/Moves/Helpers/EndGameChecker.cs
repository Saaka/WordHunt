using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Games.Repository;
using WordHunt.Models.Events;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Moves.Helpers
{
    public interface IEndGameChecker
    {
        Task<EndGameCheckResult> VerifyEndGame(CurrentGameState gameState, FieldChecked fieldChecked);
    }

    class EndGameChecker : IEndGameChecker
    {
        private readonly INextTeamProvider nextTeamProvider;
        private readonly IGameTeamRepository teamRepository;

        public EndGameChecker(INextTeamProvider nextTeamProvider,
            IGameTeamRepository teamRepository)
        {
            this.nextTeamProvider = nextTeamProvider;
            this.teamRepository = teamRepository;
        }

        public async Task<EndGameCheckResult> VerifyEndGame(CurrentGameState gameState, FieldChecked fieldChecked)
        {
            if (gameState.EndMode == Base.Enums.Game.EndMode.SuddenDeath)
                return await VerifySuddenDeathEndGame(gameState, fieldChecked);

            throw new NotImplementedException("NOT IMPLEMEMTED END MODE");
        }

        private async Task<EndGameCheckResult> VerifySuddenDeathEndGame(CurrentGameState gameState, FieldChecked fieldChecked)
        {
            var result = new EndGameCheckResult();
            if (fieldChecked.Type == Base.Enums.Game.FieldType.Trap)
            {
                result.GameEnded = true;
                var winningTeam = await nextTeamProvider.GetNextTeam(gameState.GameId, gameState.EndMode);
                result.WinningTeamId = winningTeam.Id;
            }
            else if(fieldChecked.Type == Base.Enums.Game.FieldType.Team)
            {
                var remainingFieldCount = await teamRepository.GetRemainingFieldCount(fieldChecked.TeamId);
                if (remainingFieldCount == 1)
                {
                    result.GameEnded = true;
                    result.WinningTeamId = fieldChecked.TeamId;
                }
            }

            return result;
        }
    }
}
