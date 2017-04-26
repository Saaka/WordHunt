using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameStatus
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CurrentTeamId { get; set; }
        public Status Status { get; set; }
        public bool Latest { get; set; }

        public virtual Game Game { get; set; }
    }
}
