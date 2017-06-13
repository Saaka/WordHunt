using WordHunt.Models.Events;

namespace WordHunt.Games.Broadcaster
{
    public interface IEventBroadcaster
    {
        void TeamChanged(TeamChanged args);
        void FieldChecked(FieldChecked args);
        void EndGame(GameEnded args);
        void RestartGame(GameRestarted args);
    }
}
