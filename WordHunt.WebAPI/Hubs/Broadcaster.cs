﻿using Microsoft.AspNet.SignalR;
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
        void ClientSubscribed();
    }

    [HubName("broadcaster")]
    public class Broadcaster : Hub<IBroadcaster>
    {
        public void Subscibe(int gameId)
        {
            Groups.Add(Context.ConnectionId, gameId.ToString());
        }

        public void HandleMessage(string message)
        {

            Clients.All.HandleMessage("Joined: " + message);
        }
    }
}