using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Events;

namespace WordHunt.Data.Events
{
    public class TeamChanged
    {
        public int GameId { get; set; }
        public int NewTeamId { get; set; }
        public int LastTeamId { get; set; }
        public TeamChangeReason ChangeReason { get; set; }
    }
}
