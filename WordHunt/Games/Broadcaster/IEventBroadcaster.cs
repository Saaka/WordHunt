using WordHunt.Models.Events;

namespace WordHunt.Games.Broadcaster
{
    public interface IEventBroadcaster
    {
        void TeamChanged(TeamChanged args);
    }
}
