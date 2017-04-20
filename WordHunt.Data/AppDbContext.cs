using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordHunt.Config;
using Microsoft.EntityFrameworkCore.Metadata;
using WordHunt.Data.Entities;
using System.Threading.Tasks;
using WordHunt.Data.Initializer;

namespace WordHunt.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>, 
        IAppDbContext, 
        IAppDbInitializerContext
    {
        private IAppConfiguration config;

        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }

        public AppDbContext(DbContextOptions options, IAppConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Word>()
                    .HasOne(w => w.Language)
                    .WithMany(l => l.Words)
                    .HasForeignKey(fk => fk.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Word>()
                    .HasOne(w => w.Category)
                    .WithMany(c => c.Words)
                    .HasForeignKey(fk => fk.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

            builder.Entity<Category>()
                    .HasOne(c => c.Language)
                    .WithMany(l => l.Categories)
                    .HasForeignKey(fk => fk.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(config.DbConnectionString,
                ob => ob.MigrationsHistoryTable("WordHuntMigrations"));

        }

        public Task MigrateAsync()
        {
            return Database.MigrateAsync();
        }
    }
}
