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
    }

    public class UserService : IUserService
    {
        private readonly IIdentityUserManager userManager;

        public UserService(IIdentityUserManager userManager)
        {
            this.userManager = userManager;
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
    }
}
