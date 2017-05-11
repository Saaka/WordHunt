using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordHunt.Games.Repository;

namespace WordHunt.WebAPI.Controllers.Games.Access
{
    [Route("api/game")]
    public class GameAccessController : Controller
    {
        private readonly IGameRepository gameProvider;

        public GameAccessController(IGameRepository gameProvider)
        {
            this.gameProvider = gameProvider;
        }

        [HttpGet("{gameId}")]
        public async Task<WordHunt.Models.Games.Access.Game> GetGameFull(int gameId)
        {
            return await gameProvider.GetCompleteGameInfo(gameId);
        }
    }
}
