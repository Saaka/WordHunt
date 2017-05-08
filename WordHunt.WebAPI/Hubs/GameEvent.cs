using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordHunt.WebAPI.Hubs
{
    public class GameEvent
    {
        public GameEvent()
        {

        }

        public int GameId { get; set; }
        public DateTime TimeStamp { get; private set; }

    }
}
