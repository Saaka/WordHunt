using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameMove
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long FieldId { get; set; }
        public MoveType Type { get; set; }
        public long TeamId { get; set; }
    }
}
