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
            BaseGameValidations(gameState, userId);
        }


        public void ValidateFieldCheck(CurrentGameState gameState, CurrentFieldState fieldState, int userId)
        {
            BaseGameValidations(gameState, userId);

            if (fieldState.Checked)
                throw new ValidationException("This field was already checked");
        }

        protected void BaseGameValidations(CurrentGameState gameState, int userId)
        {
            if (gameState.Type != Base.Enums.Game.GameType.SingleDevice)
                throw new InvalidOperationException("Wrong validator seleceted for game type");

            if (gameState.UserId != userId)
                throw new ValidationException("Current user can't do this action");

            if (gameState.Status == Base.Enums.Game.Status.Canceled || gameState.Status == Base.Enums.Game.Status.Finished)
                throw new ValidationException("Game has ended");
        }
    }
}
