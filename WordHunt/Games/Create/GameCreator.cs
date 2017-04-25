using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Games;
using WordHunt.DataInterfaces.Games.DTO;
using WordHunt.DataInterfaces.Games.Result;

namespace WordHunt.Games.Create
{
    public class GameCreator : IGameCreator
    {
        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            try
            {

                return new GameCreateResult();
            }
            catch(Exception ex)
            {
                return new GameCreateResult(ex.ToString());
            }
        }
    }
}
