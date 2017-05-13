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
        void HandleMessage(string message);
        void Subscribed(string message);
    }

    [HubName("broadcaster")]
    public class Broadcaster : Hub<IBroadcaster>
    {
        public void Subscribe(int gameId)
        {
            Groups.Add(Context.ConnectionId, gameId.ToString());

            Clients.OthersInGroup(gameId.ToString()).HandleMessage("New client joined");

            Clients.Caller.Subscribed("Subscribed to game");
        }

        public void HandleMessage(string message)
        {

            Clients.All.HandleMessage("Joined: " + message);
        }
    }
}
