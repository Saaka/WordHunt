using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.WebAPI.Models;
using Microsoft.Extensions.Logging;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.WebAPI.Filters;

namespace WordHunt.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private ITokenGenerator tokenGenerator;
        private ILogger<AuthController> logger;

        public AuthController(ITokenGenerator tokenGenerator,
            ILogger<AuthController> logger)
        {
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
        }

        [ValidateModel]
        [HttpPost("token")]
        public async Task<TokenGeneratorResult> CreateToken([FromBody] CredentialsModel model)
        {
            var result = await tokenGenerator.GenerateToken(model.UserName, model.Password);

            return result;
        }
    }
}