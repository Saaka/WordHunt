using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.Games.Moves;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Game.Creation
{
    [Authorize]
    [Route("api/game")]
    public class GameModificationController : Controller
    {
        private readonly ITokenUserContextProvider userProvider;
        private readonly IGameMoveManager gameMoveManager;

        public GameModificationController(ITokenUserContextProvider userProvider,
            IGameMoveManager gameMoveManager)
        {
            this.userProvider = userProvider;
            this.gameMoveManager = gameMoveManager;
        }

        [Authorize]
        [HttpGet("{gameId}/skipround")]
        public async Task<IActionResult> SkipRound(int gameId)
        {
            var currentUser = userProvider.GetContextUserInfo();

            var result = await gameMoveManager.SkipRound(gameId, currentUser.Id);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{gameId}/field/{fieldId}/check")]
        public async Task<IActionResult> CheckField(int gameId, int fieldId)
        {
            var currentUser = userProvider.GetContextUserInfo();

            var result = await gameMoveManager.CheckField(gameId, currentUser.Id, fieldId);

            return Ok(result);
        }
    }
}
