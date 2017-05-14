using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordHunt.WebAPI.Hubs
{
    public interface IBroadcaster
    {
        //void Subscribed(string message);
    }

    [HubName("broadcaster")]
    public class Broadcaster : Hub<IBroadcaster>
    {
        public string Subscribe(int gameId)
        {
            Groups.Add(Context.ConnectionId, gameId.ToString());

            return Context.ConnectionId;
        }
    }
}
