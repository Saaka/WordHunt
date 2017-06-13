using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Models.Events
{
    public class GameRestarted
    {
        public int GameId { get; set; }
        public int OldGameId { get; set; }
    }
}
