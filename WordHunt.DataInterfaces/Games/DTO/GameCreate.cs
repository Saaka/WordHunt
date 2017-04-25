using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.DataInterfaces.Games.DTO
{
    public class GameCreate
    {
        public string Name { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int TeamCount { get; set; }
        public int TrapCount { get; set; }
        public GameType Type { get; set; }
        public EndMode EndMode { get; set; }
        public int UserId { get; set; }

        public IEnumerable<GameTeamCreate> Teams { get; set; }
    }
}
