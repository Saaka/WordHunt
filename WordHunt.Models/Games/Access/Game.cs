using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Access
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int TeamCount { get; set; }
        public GameType Type { get; set; }

        public IEnumerable<BoardField> Fields { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
