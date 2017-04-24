using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
        public int TeamCount { get; set; }
        public int TrapCount { get; set; }
        public GameType Type { get; set; }
    }
}
