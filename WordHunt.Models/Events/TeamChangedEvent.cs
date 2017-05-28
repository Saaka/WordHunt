using WordHunt.Base.Enums.Events;

namespace WordHunt.Models.Events
{
    public class TeamChanged
    {
        public int GameId { get; set; }
        public int NewTeamId { get; set; }
        public int LastTeamId { get; set; }
        public TeamChangeReason ChangeReason { get; set; }
    }
}
