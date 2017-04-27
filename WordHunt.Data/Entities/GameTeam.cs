using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class GameTeam
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public int FieldCount { get; set; }
        public int RemainingFieldCount { get; set; }

        public virtual Game Game { get; set; }
    }
}
