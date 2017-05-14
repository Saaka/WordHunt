using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Access
{
    public class LatestStatus
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CurrentTeamId { get; set; }
        public bool Latest { get; set; }
        public Status Status { get; set; }
    }
}
