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

namespace WordHunt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private ILogger<AuthController> logger;
        private IPasswordHasher<IdentityUser> hasher;
        private IAuthConfiguration authConfig;

        public AuthController(UserManager<IdentityUser> userManager,
            ILogger<AuthController> logger,
            IPasswordHasher<IdentityUser> hasher,
            IAuthConfiguration authConfig)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.hasher = hasher;
            this.authConfig = authConfig;
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialsModel model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await userManager.GetClaimsAsync(user);
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        }.Union(userClaims);

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.TokenKey));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: authConfig.Issuer,
                            audience: authConfig.Audience,
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(authConfig.TokenValidInMinutes),
                            signingCredentials: credentials);

                        return Ok(new TokenModel(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while creating token {ex.Message}", ex);
            }

            return BadRequest("Failed to generate token");
        }
    }
}