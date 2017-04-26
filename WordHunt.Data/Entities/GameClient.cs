using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameClient
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public ClientType Type { get; set; }
        public int? TeamId { get; set; }
    }
}
