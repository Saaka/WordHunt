using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameClient
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public ClientType Type { get; set; }
        public long? TeamId { get; set; }
    }
}
