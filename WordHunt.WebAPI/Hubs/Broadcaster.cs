using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordHunt.WebAPI.Hubs
{
    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
    }

    [HubName("boradcaster")]
    public class Broadcaster : Hub//<IBroadcaster>
    {
        //public override Task OnConnected()
        //{
        //    return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
        //}

        public async void SendMessage(string message)
        {
            await Clients.All.newMessage($"{DateTime.Now} : { message}");
        }

        public async void Subscribe(int gameId)
        {
            await Groups.Add(Context.ConnectionId, gameId.ToString());
        }

        public async void  Unsubscribe(int gameId)
        {
            await Groups.Remove(Context.ConnectionId, gameId.ToString());
        }
    }
}
