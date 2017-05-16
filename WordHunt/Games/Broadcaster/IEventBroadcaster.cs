using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Events;

namespace WordHunt.Games.Broadcaster
{
    public interface IEventBroadcaster
    {
        void TeamChanged(TeamChanged args);
    }
}
