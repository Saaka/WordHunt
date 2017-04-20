using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Entities;

namespace WordHunt.Data
{
    public interface IAppDbContext
    {
        DbSet<Word> Words { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Language> Languages { get; set; }
        Task<int> SaveChangesAsync();
    }
}
