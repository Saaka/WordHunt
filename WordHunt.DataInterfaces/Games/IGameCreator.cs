using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Games.DTO;
using WordHunt.Interfaces.Games.Result;

namespace WordHunt.Interfaces.Games
{
    public interface IGameCreator
    {
        Task<GameCreateResult> CreateGame(GameCreate game);
    }
}
