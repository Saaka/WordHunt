using Microsoft.AspNet.SignalR;
using WordHunt.Models.Events;
using WordHunt.Games.Broadcaster;

namespace WordHunt.WebAPI.Hubs
{
    public class EventBroadcaster : IEventBroadcaster
    {
        public void TeamChanged(TeamChanged args)
        {
            HubContext.Clients.Group(args.GameId.ToString()).TeamChanged(args);
        }

        IHubContext<IEventClient> HubContext { get; } = GlobalHost.ConnectionManager.GetHubContext<Broadcaster, IEventClient>();
    }
}
