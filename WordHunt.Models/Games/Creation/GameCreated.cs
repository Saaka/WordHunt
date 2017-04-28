using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Models.Games.Creation
{
    public class GameCreated
    {
        public int Id { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int TrapCount { get; set; }
        public int LanguageId { get; set; }
    }
}
