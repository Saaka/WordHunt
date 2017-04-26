using System;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameMove
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int FieldId { get; set; }
        public MoveType Type { get; set; }
        public int TeamId { get; set; }
        public int ClientId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
