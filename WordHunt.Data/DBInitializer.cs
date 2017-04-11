using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Config;
using WordHunt.Data.Entities;

namespace WordHunt.Data
{
    public interface IDBInitializer
    {
        Task InitDatabase();
    }

    public class DBInitializer : IDBInitializer
    {
        private AppDbContext context;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        private ISeedConfiguration seedConfig;

        public DBInitializer(AppDbContext context,
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
            var user = await userManager.FindByEmailAsync(seedConfig.AdminEmail);

            if (user == null)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "isAdmin", ClaimValue = "true" });
                    await roleManager.CreateAsync(role);
                }

                user = new IdentityUser()
                {
                    UserName = seedConfig.AdminName,
                    Email = seedConfig.AdminEmail
                };

                var userResult = await userManager.CreateAsync(user, seedConfig.AdminPassword);
                var roleResult = await userManager.AddToRoleAsync(user, "Admin");
                var claimResult = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("isAdmin", "true", System.Security.Claims.ClaimValueTypes.Boolean));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }
}
