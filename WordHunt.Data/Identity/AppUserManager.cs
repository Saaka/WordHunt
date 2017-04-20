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
        Task<AppUser> FindUserByNameAsync(string userName);
        Task<IList<Claim>> GetClaimsAsync(AppUser user);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim);
    }

    public class AppUserManager : IAppUserManager
    {
        private readonly IAppDbContext context;
        private readonly ILookupNormalizer keyNormalizer;
        private readonly UserManager<AppUser> userManager;

        public AppUserManager(IAppDbContext context,
            ILookupNormalizer keyNormalizer,
            UserManager<AppUser> userManager)
        {
            this.context = context;
            this.keyNormalizer = keyNormalizer;
            this.userManager = userManager;
        }

        public async Task<AppUser> FindUserByNameAsync(string userName)
        {
            string normalizedName = keyNormalizer.Normalize(userName);

            return await context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedName);
        }

        public async Task<IList<Claim>> GetClaimsAsync(AppUser user)
        {
            return await context.UserClaims.Where(uc => uc.UserId.Equals(user.Id)).Select(c => c.ToClaim()).ToListAsync();
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        
        public async Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim)
        {
            return await userManager.AddClaimAsync(user, claim);
        }
    }
}
