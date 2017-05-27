using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Moves.Validation
{
    public class SingleDeviceMoveValidator : IMoveValidator
    {
        public void ValidateRoundSkip(CurrentGameState gameState, int userId)
        {
            if (gameState.UserId != userId)
                throw new ValidationException("Current user can't pass this turn");

            if (gameState.Type != Base.Enums.Game.GameType.SingleDevice)
                throw new InvalidOperationException("Wrong validator seleceted for game type");

            if (gameState.Status == Base.Enums.Game.Status.Canceled || gameState.Status == Base.Enums.Game.Status.Finished)
                throw new ValidationException("Game has ended");
        }


        public void ValidateFieldCheck(CurrentGameState gameState, int userId)
        {
        }
    }
}
