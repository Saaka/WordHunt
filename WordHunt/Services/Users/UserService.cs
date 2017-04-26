using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Identity;
using WordHunt.Interfaces.Users;
using WordHunt.Interfaces.Users.DTO;

namespace WordHunt.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IAppUserManager userManager;
        private readonly IPasswordHasher<Data.Entities.User> hasher;

        public UserService(IAppUserManager userManager,
            IPasswordHasher<Data.Entities.User> hasher)
        {
            this.userManager = userManager;
            this.hasher = hasher;
        }

        public async Task<User> LoadUserByName(string userName)
        {
            var entity = await userManager.FindUserByNameAsync(userName);
            if (entity == null) return null;

            return new User()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.UserName
            };
        }

        public async Task<bool> ValidatePasswordForUser(User user, string password)
        {
            var entity = await userManager.GetUserByIdAsync(user.Id);
            if (entity == null) throw new ArgumentException($"User not found for id {user.Id}");

            return hasher.VerifyHashedPassword(entity, entity.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}
