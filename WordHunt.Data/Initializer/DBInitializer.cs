using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Config;
using WordHunt.Config.Auth;
using WordHunt.Data.Entities;

using WordHunt.Data.Identity;

namespace WordHunt.Data.Initializer
{
    public interface IAppDbInitializerContext : IAppDbContext
    {
        Task MigrateAsync();
    }

    public interface IDBInitializer
    {
        Task InitDatabase();
    }

    public class DBInitializer : IDBInitializer
    {
        private IAppDbInitializerContext context;
        private IAppRoleManager roleManager;
        private IAppUserManager userManager;
        private ISeedConfiguration seedConfig;

        public DBInitializer(IAppDbInitializerContext context,
            IAppUserManager userManager,
            IAppRoleManager roleManager,
            ISeedConfiguration seedConfig)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.seedConfig = seedConfig;
        }

        public async Task InitDatabase()
        {
            await context.MigrateAsync();

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
            await CreateUser(seedConfig.AdminName, seedConfig.AdminEmail, seedConfig.AdminPassword, true);
            await CreateUser(seedConfig.UserName, seedConfig.UserEmail, seedConfig.UserPassword, false);
        }

        private async Task CreateUser(string name, string email, string password, bool isAdmin)
        {
            var user = await userManager.FindUserByNameAsync(name);
            if (user == null)
            {
                user = new User()
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
                var role = new Role(SystemRoles.Admin);
                role.Claims.Add(new IdentityRoleClaim<int>() { ClaimType = SystemClaims.IsAdmin, ClaimValue = "true" });
                await roleManager.CreateAsync(role);
            }
        }
    }
}
