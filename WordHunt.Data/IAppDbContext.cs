using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WordHunt.Data.Entities;

namespace WordHunt.Data
{
    public interface IAppDbContext
    {
        DbSet<Word> Words { get; set; }
        DbSet<AppUser> Users { get; set; }
        DbSet<IdentityUserClaim<long>> UserClaims { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Language> Languages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
