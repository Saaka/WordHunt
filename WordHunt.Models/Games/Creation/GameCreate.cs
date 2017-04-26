using System.Collections.Generic;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Creation
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
