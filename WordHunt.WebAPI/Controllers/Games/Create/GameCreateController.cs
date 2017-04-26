using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Games.Create;
using WordHunt.Models.Games.Creation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Game
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

        // POST api/values
        [HttpPost("create")]
        public async Task<GameCreateResult> Post([FromBody]GameCreate model)
        {
            return await gameCreator.CreateGame(model);
        }
    }
}
