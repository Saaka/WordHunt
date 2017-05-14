using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Access
{
    public class CurrentGameState
    {
        public int UserId { get; set; }
        public EndMode EndMode { get; set; }
        public GameType Type { get; set; }
        public int CurrentTeamId { get; set; }
        public Status Status { get; set; }
    }
}
