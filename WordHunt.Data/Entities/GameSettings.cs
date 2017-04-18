using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameSettings
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public string Name { get; set; }
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
        public int TeamCount { get; set; }
        public int TrapCount { get; set; }
        public GameType Type { get; set; }
    }
}
