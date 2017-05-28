using Microsoft.AspNet.SignalR;
using WordHunt.Models.Events;
using WordHunt.Games.Broadcaster;
using System;

namespace WordHunt.WebAPI.Hubs
{
    public class EventBroadcaster : IEventBroadcaster
    {
        public void TeamChanged(TeamChanged args)
        {
            HubContext.Clients.Group(args.GameId.ToString()).TeamChanged(args);
        }

        public void FieldChecked(FieldChecked args)
        {
            HubContext.Clients.Group(args.GameId.ToString()).FieldChecked(args);
        }

        IHubContext<IEventClient> HubContext { get; } = GlobalHost.ConnectionManager.GetHubContext<Broadcaster, IEventClient>();
    }
}
