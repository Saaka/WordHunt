using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.WebAPI.Models;
using Microsoft.Extensions.Logging;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WordHunt.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private ITokenGenerator tokenGenerator;
        private ILogger<AuthController> logger;
        ITokenUserContextProvider userProvider;

        public AuthController(ITokenGenerator tokenGenerator,
            ILogger<AuthController> logger,
            ITokenUserContextProvider userProvider)
        {
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
            this.userProvider = userProvider;
        }

        [ValidateModel]
        [HttpPost("token")]
        public async Task<TokenGeneratorResult> CreateToken([FromBody] CredentialsModel model)
        {
            var result = await tokenGenerator.GenerateToken(model.UserName, model.Password);

            return result;
        }

        [Authorize]
        [HttpGet("check")]
        public IActionResult Get()
        {
            return Ok(userProvider.GetContextUserInfo());
        }
    }
}