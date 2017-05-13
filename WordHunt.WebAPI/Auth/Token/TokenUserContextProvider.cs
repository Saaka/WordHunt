using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WordHunt.WebAPI.Auth.Token
{
    public interface ITokenUserContextProvider
    {
        UserInfo GetContextUserInfo();
    }

    public class TokenUserContextProvider : ITokenUserContextProvider
    {
        private ClaimsPrincipal caller;

        public TokenUserContextProvider(ClaimsPrincipal caller)
        {
            this.caller = caller;
        }

        public UserInfo GetContextUserInfo()
        {
            if (caller == null || caller.Identity == null)
                throw new InvalidOperationException("No user specified information");
            
            var identity = caller.Identity as ClaimsIdentity;
            if (identity == null || !caller.Identity.IsAuthenticated)
                throw new ArgumentException("User is not properly authenticated");

            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "id");
            if (idClaim == null)
                throw new ArgumentException("No User information provided white authenticating");
            
            return new UserInfo()
            {
                Id = Convert.ToInt32(idClaim.Value)
            };
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
    }
}
