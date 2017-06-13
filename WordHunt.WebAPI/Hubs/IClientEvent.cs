using WordHunt.Models.Events;

namespace WordHunt.WebAPI.Hubs
{
    /// <summary>
    /// Actions that can be invoke on the client from server.
    /// </summary>
    public interface IEventClient
    {
        void TeamChanged(TeamChanged args);
        void FieldChecked(FieldChecked args);
        void GameEnded(GameEnded args); 
        void GameRestarted(GameRestarted args); 
    }
}
