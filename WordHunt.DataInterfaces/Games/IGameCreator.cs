using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Games.DTO;
using WordHunt.DataInterfaces.Games.Result;

namespace WordHunt.DataInterfaces.Games
{
    public interface IGameCreator
    {
        Task<GameCreateResult> CreateGame(GameCreate game);
    }
}
