using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WordHunt.Data.Identity;
using WordHunt.Models.Users;

namespace WordHunt.Services.Users
{
    public interface IUserService
    {
        Task<UserModel> LoadUserByName(string userName);
        Task<bool> ValidatePasswordForUser(UserModel user, string password);
    }

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

        public async Task<UserModel> LoadUserByName(string userName)
        {
            var entity = await userManager.FindUserByNameAsync(userName);
            if (entity == null) return null;

            return new UserModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.UserName
            };
        }

        public async Task<bool> ValidatePasswordForUser(UserModel user, string password)
        {
            var entity = await userManager.GetUserByIdAsync(user.Id);
            if (entity == null) throw new ArgumentException($"User not found for id {user.Id}");

            return hasher.VerifyHashedPassword(entity, entity.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}
