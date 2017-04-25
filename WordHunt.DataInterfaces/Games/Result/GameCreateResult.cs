using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Common;

namespace WordHunt.DataInterfaces.Games.Result
{
    public class GameCreateResult : RequestResult
    {
        public GameCreateResult() : base() { }

        public GameCreateResult(string error) : base(error) { }

        public int GameId { get; set; }
    }
}
