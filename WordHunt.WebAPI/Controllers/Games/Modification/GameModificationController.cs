using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.WebAPI.Auth.Token;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Game.Creation
{
    [Authorize]
    [Route("api/game")]
    public class GameModificationController : Controller
    {
        private readonly ITokenUserContextProvider userProvider;
        public GameModificationController(ITokenUserContextProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        [Authorize]
        [HttpPost("passturn")]
        public async Task<IActionResult> PassTurn(int gameId)
        {
            var currentUser = userProvider.GetContextUserInfo();


            return Ok();
        }
    }
}
