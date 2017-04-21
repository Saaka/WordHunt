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
using WordHunt.Data.Identity;
using WordHunt.Data.Entities;

namespace WordHunt.WebAPI.Auth.Token
{
    public interface ITokenGenerator
    {
        Task<TokenGeneratorResult> GenerateToken(string userName, string password);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private IAppUserManager userManager;
        private IPasswordHasher<User> hasher;
        private IAuthConfiguration authConfig;

        public TokenGenerator(IAppUserManager userManager,
            IPasswordHasher<User> hasher,
            IAuthConfiguration authConfig)
        {
            this.userManager = userManager;
            this.hasher = hasher;
            this.authConfig = authConfig;
        }

        public async Task<TokenGeneratorResult> GenerateToken(string userName, string password)
        {
            var user = await userManager.FindUserByNameAsync(userName);
            if (user != null)
            {
                if (hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
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
                        ErrorMessage = $"Password for email {userName} is invalid."
                    };
                }
            }
            else
            {
                return new TokenGeneratorResult
                {
                    ResultStatus = TokenGeneratorResultStatus.UserNotFound,
                    ErrorMessage = $"User for email {userName} not found"
                };
            }
        }
    }
}
