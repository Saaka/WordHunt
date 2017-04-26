using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Users.DTO;

namespace WordHunt.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> LoadUserByName(string userName);
        Task<bool> ValidatePasswordForUser(User user, string password);
    }
}
