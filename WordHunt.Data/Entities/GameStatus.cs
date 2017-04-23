using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameStatus
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CurrentTeamId { get; set; }
        public GameStatus Status { get; set; }
    }
}
