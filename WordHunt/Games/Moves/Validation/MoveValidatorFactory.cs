using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Games.Moves.Validation
{
    public interface IMoveValidatorFactory
    {
        IMoveValidator GetMoveValidator(GameType gameType);
    }

    public class MoveValidatorFactory : IMoveValidatorFactory
    {
        public IMoveValidator GetMoveValidator(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.SingleDevice:
                    return new SingleDeviceMoveValidator();
                default:
                    throw new InvalidOperationException("Not supported game type. Can't get move validator.");
            }
        }
    }
}
