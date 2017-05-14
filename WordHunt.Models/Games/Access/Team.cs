using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Models.Games.Access
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public int FieldCount { get; set; }
        public int RemainingFieldCount { get; set; }
    }

    public class NextTeam
    {
        public int Id { get; set; }
        public int Order { get; set; }
    }
}
