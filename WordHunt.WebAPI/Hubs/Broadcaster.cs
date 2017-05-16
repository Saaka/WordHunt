using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Games.Broadcaster;
using WordHunt.Data.Events;

namespace WordHunt.WebAPI.Hubs
{
    /// <summary>
    /// Actions that can be invoke on the client from server/.
    /// </summary>
    public interface IEventClient
    {
        void TeamChanged(TeamChanged args);
    }

    /// <summary>
    /// Action than client side can invoke.
    /// </summary>
    public interface IEventServer
    {
        string Subscribe(int gameId);
    }
    
    [HubName("broadcaster")]
    public class Broadcaster : Hub<IEventClient>, IEventServer
    {
        public string Subscribe(int gameId)
        {
            Groups.Add(Context.ConnectionId, gameId.ToString());

            return Context.ConnectionId;
        }

        //public void TeamChanged(TeamChanged args)
        //{
        //    Clients.Group(args.GameId.ToString()).TeamChanged(args);
        //}
    }
}
