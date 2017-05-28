using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WordHunt.Models.Events;

namespace WordHunt.WebAPI.Hubs
{    
    [HubName("broadcaster")]
    public class Broadcaster : Hub<IEventClient>, IEventServer
    {
        public string Subscribe(int gameId)
        {
            Groups.Add(Context.ConnectionId, gameId.ToString());
            
            return Context.ConnectionId;
        }
    }
}
