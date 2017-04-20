using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class Game
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CurrentTeamId { get; set; }
        public GameStatus Status { get; set; }
    }
}
