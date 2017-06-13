using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Games.Creation;
using WordHunt.Models.Games.Creation;

namespace WordHunt.WebAPI.Controllers.Game.Creation
{
    [Authorize]
    [Route("api/game")]
    public class GameCreateController : Controller
    {
        private readonly IGameCreator gameCreator;

        public GameCreateController(IGameCreator gameCreator)
        {
            this.gameCreator = gameCreator;
        }
        
        [HttpPost("create")]
        public async Task<GameCreateResult> Create([FromBody]GameCreate model)
        {
            return await gameCreator.CreateGame(model);
        }
        
        [HttpGet("{gameId}/restart")]
        public async Task<GameCreateResult> Restart(int gameId)
        {
            return await gameCreator.CreateGameBasedOnAnother(gameId);
        }
    }
}
