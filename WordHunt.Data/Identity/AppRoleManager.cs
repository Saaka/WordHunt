using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Entities;

namespace WordHunt.Data.Identity
{
    public interface IAppRoleManager
    {
        Task<bool> RoleExistsAsync(string role);
        Task<IdentityResult> CreateAsync(Role role);
    } 

    public class AppRoleManager : IAppRoleManager
    {
        private readonly RoleManager<Role> roleManager;

        public AppRoleManager(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await roleManager.RoleExistsAsync(role);
        }

        public async Task<IdentityResult> CreateAsync(Role role)
        {
            return await roleManager.CreateAsync(role);
        }
    }
}
