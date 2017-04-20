using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Config;
using WordHunt.Config.Auth;
using WordHunt.Data.Entities;

namespace WordHunt.Data
{
    public interface IDBInitializer
    {
        Task InitDatabase();
    }

    public class DBInitializer : IDBInitializer
    {
        private IAppDbContext context;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        private ISeedConfiguration seedConfig;

        public DBInitializer(IAppDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ISeedConfiguration seedConfig)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.seedConfig = seedConfig;
        }

        public async Task InitDatabase()
        {
            await context.Database.MigrateAsync();

            await CreateUserAndRoles();

            await CreateDictionary();
        }

        private async Task CreateDictionary()
        {
            if (await context.Languages.AnyAsync())
                return;

            var language = new Language()
            {
                Code = "PL",
                Name = "Polski"
            };
            var category = new Category()
            {
                Name = "Zwierzęta",
                Language = language
            };
            var animals = new string[] { "Kot", "Pies" };

            animals.ToList().ForEach(x => {
                var word = CreateWord(x, language, category);
                context.Words.Add(word);
            });            

            await context.SaveChangesAsync();
        }

        private Word CreateWord(string value, Language language, Category category = null)
        {
            var word = new Word()
            {
                Value = value,
                Language = language
            };

            if (category != null)
                word.Category = category;

            return word;
        }

        private async Task CreateUserAndRoles()
        {
            await CreateAdminRole();
            await CreateUser(seedConfig.AdminEmail, seedConfig.AdminName, seedConfig.AdminPassword, true);
            await CreateUser(seedConfig.UserEmail, seedConfig.UserName, seedConfig.UserPassword, false);
        }

        private async Task CreateUser(string email, string name, string password, bool isAdmin)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = name,
                    Email = email
                };

                var userResult = await userManager.CreateAsync(user, password);
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

                if (isAdmin)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, SystemRoles.Admin);
                    var claimResult = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(SystemClaims.IsAdmin, "true", System.Security.Claims.ClaimValueTypes.Boolean));
                    if (!roleResult.Succeeded || !claimResult.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to build user and roles");
                    }
                }
            }
        }

        private async Task CreateAdminRole()
        {
            if (!await roleManager.RoleExistsAsync(SystemRoles.Admin))
            {
                var role = new IdentityRole(SystemRoles.Admin);
                role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = SystemClaims.IsAdmin, ClaimValue = "true" });
                await roleManager.CreateAsync(role);
            }
        }
    }
}
