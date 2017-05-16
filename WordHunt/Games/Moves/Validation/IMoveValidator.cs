using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Moves.Validation
{
    public interface IMoveValidator
    {
        void ValidateRoundSkip(CurrentGameState gameState, int userId);
    }
}
