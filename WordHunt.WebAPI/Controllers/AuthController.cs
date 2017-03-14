using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordHunt.Config;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.WebAPI.Filters;

namespace WordHunt.WebAPI.Controllers
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
public async Task<IActionResult> CreateToken([FromBody] CredentialsModel model)
{
    try
    {
        var result = await tokenGenerator.GenerateToken(model.Email, model.Password);
        if (result.ResultStatus == TokenGeneratorResultStatus.Success)
            return Ok(result.Token);
        else
        {
            logger.LogWarning($"Failed to generate the token: {result.ErrorMessage}");
            return BadRequest("Failed to generate token");
        }
    }
    catch (Exception ex)
    {
        logger.LogError($"Error occured while creating token {ex.Message}", ex);
        return StatusCode(500);
    }
}
    }
}