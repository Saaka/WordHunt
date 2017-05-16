using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data.Events;
using WordHunt.Games.Broadcaster;

namespace WordHunt.WebAPI.Hubs
{
    public class EventBroadcaster : IEventBroadcaster
    {
        public EventBroadcaster()
        {

        }

        public void TeamChanged(TeamChanged args)
        {
            HubContext.Clients.Group(args.GameId.ToString()).TeamChanged(args);
        }

        IHubContext<IEventClient> HubContext { get; } = GlobalHost.ConnectionManager.GetHubContext<Broadcaster, IEventClient>();
    }
}
