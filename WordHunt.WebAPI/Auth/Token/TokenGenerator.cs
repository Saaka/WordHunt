using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordHunt.Config;
using WordHunt.WebAPI.Models;
using WordHunt.Interfaces.Users;
using WordHunt.Data.Identity;

namespace WordHunt.WebAPI.Auth.Token
{
    public interface ITokenGenerator
    {
        Task<TokenGeneratorResult> GenerateToken(string userName, string password);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private IAuthConfiguration authConfig;
        private IUserService userService;
        private IAppUserClaimsProvider claimsProvider;

        public TokenGenerator(IUserService userService,
            IAuthConfiguration authConfig,
            IAppUserClaimsProvider claimsProvider)
        {
            this.userService = userService;
            this.authConfig = authConfig;
            this.claimsProvider = claimsProvider;
        }

        public async Task<TokenGeneratorResult> GenerateToken(string userName, string password)
        {
            var user = await userService.LoadUserByName(userName);
            if (user != null)
            {
                if (await userService.ValidatePasswordForUser(user, password))
                {
                    var userClaims = await claimsProvider.GetClaimsAsync(user.Id);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id.ToString())
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
