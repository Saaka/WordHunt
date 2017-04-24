using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Users.DTO;

namespace WordHunt.DataInterfaces.Users
{
    public interface IUserService
    {
        Task<User> LoadUserByName(string userName);
        Task<bool> ValidatePasswordForUser(User user, string password);
    }
}
