using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Games.Moves.Helpers
{
    public class EndGameCheckResult
    {
        public bool GameEnded { get; set; }
        public int? WinningTeamId { get; set; }
    }
}
