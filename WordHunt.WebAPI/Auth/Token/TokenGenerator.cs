using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordHunt.Config;
using WordHunt.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WordHunt.WebAPI.Auth.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private UserManager<IdentityUser> userManager;
        private IPasswordHasher<IdentityUser> hasher;
        private IAuthConfiguration authConfig;

        public TokenGenerator(UserManager<IdentityUser> userManager,
            IPasswordHasher<IdentityUser> hasher,
            IAuthConfiguration authConfig)
        {
            this.userManager = userManager;
            this.hasher = hasher;
            this.authConfig = authConfig;
        }

        public async Task<TokenGeneratorResult> GenerateToken(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await userManager.GetClaimsAsync(user);
                    var roles = await userManager.GetRolesAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("admin", roles.Contains("Admin").ToString())
                     }.Union(userClaims);

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.TokenKey));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: authConfig.Issuer,
                        audience: authConfig.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(authConfig.TokenValidInMinutes),
                        signingCredentials: credentials);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return new TokenGeneratorResult
                    {
                        ResultStatus = TokenGeneratorResultStatus.Success,
                        Token = new TokenModel(tokenString, token.ValidTo)
                    };
                }
                else
                {
                    return new TokenGeneratorResult
                    {
                        ResultStatus = TokenGeneratorResultStatus.InvalidPassword,
                        ErrorMessage = $"Password for email {email} is invalid."
                    };
                }
            }
            else
            {
                return new TokenGeneratorResult
                {
                    ResultStatus = TokenGeneratorResultStatus.UserNotFound,
                    ErrorMessage = $"User for email {email} not found"
                };
            }
        }
    }
}
