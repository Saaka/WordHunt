using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WordHunt.Data.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WordHunt.Data.Identity
{
    public interface IAppUserManager
    {
        Task<User> FindUserByNameAsync(string userName);
        Task<IList<Claim>> GetClaimsAsync(User user);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddClaimAsync(User user, Claim claim);
    }

    public class AppUserManager : IAppUserManager
    {
        private readonly IAppDbContext context;
        private readonly ILookupNormalizer keyNormalizer;
        private readonly UserManager<User> userManager;

        public AppUserManager(IAppDbContext context,
            ILookupNormalizer keyNormalizer,
            UserManager<User> userManager)
        {
            this.context = context;
            this.keyNormalizer = keyNormalizer;
            this.userManager = userManager;
        }

        public async Task<User> FindUserByNameAsync(string userName)
        {
            string normalizedName = keyNormalizer.Normalize(userName);

            return await context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedName);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return await userManager.GetClaimsAsync(user);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        
        public async Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            return await userManager.AddClaimAsync(user, claim);
        }
    }
}
