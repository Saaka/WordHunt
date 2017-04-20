using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class GameTeam
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
    }
}
