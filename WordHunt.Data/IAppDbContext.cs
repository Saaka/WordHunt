using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordHunt.Data.Entities;
using WordHunt.Data.Entities.Identity;

namespace WordHunt.Data
{
    public interface IAppDbContext
    {
        DbSet<Word> Words { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Language> Languages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
