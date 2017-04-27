using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WordHunt.Data.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;

namespace WordHunt.Data.Identity
{
    public interface IIdentityUserClaimsProvider
    {
        Task<IList<Claim>> GetClaimsAsync(int userId);
    }

    public interface IIdentityUserManager : IIdentityUserClaimsProvider
    {
        Task<User> FindUserByNameAsync(string userName);        
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddClaimAsync(User user, Claim claim);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> ValidatePasswordForUser(User user, string password);
    }

    public class IdentityUserManager : IIdentityUserManager
    {
        private readonly IAppDbContext context;
        private readonly ILookupNormalizer keyNormalizer;
        private readonly UserManager<User> userManager;
        private readonly IPasswordHasher<Data.Entities.User> hasher;

        public IdentityUserManager(IAppDbContext context,
            ILookupNormalizer keyNormalizer,
            UserManager<User> userManager,
            IPasswordHasher<Data.Entities.User> hasher)
        {
            this.context = context;
            this.keyNormalizer = keyNormalizer;
            this.userManager = userManager;
            this.hasher = hasher;
        }

        public async Task<bool> ValidatePasswordForUser(User user, string password)
        {
            if (user == null)
                throw new ArgumentException("No user provided");

            return hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }

        public async Task<User> FindUserByNameAsync(string userName)
        {
            string normalizedName = keyNormalizer.Normalize(userName);

            return await context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedName);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Claim>> GetClaimsAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User not found for id {userId}.");
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
