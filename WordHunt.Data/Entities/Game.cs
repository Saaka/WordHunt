using System;
using System.Collections.Generic;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int TeamCount { get; set; }
        public int TrapCount { get; set; }
        public int LanguageId { get; set; }
        public GameType Type { get; set; }
        public EndMode EndMode { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual IList<GameTeam> Teams { get; set; }
        public virtual IList<GameStatus> Statuses { get; set; }
        public virtual IList<GameField> Fields { get; set; }
        public virtual IList<GameMove> Moves { get; set; }

        public virtual Language Language { get; set; }
    }
}
