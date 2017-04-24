using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordHunt.Config;
using Microsoft.EntityFrameworkCore.Metadata;
using WordHunt.Data.Entities;
using System.Threading.Tasks;
using WordHunt.Data.Initializer;
using System;

namespace WordHunt.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>,
        IAppDbContext,
        IAppDbInitializerContext
    {
        private IAppConfiguration config;

        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Game> GameTeams { get; set; }

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

            InitGameTable(builder);
            InitGameTeamsTable(builder);
        }

        private void InitGameTeamsTable(ModelBuilder builder)
        {
            builder.Entity<GameTeam>()
                .HasKey(x => x.Id);

            builder.Entity<GameTeam>()
                .HasOne(g => g.Game)
                .WithMany(g => g.Teams)
                .HasForeignKey(g => g.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GameTeam>()
                .Property(x => x.FieldCount)
                .IsRequired();

            builder.Entity<GameTeam>()
                .Property(x => x.Name)
                .IsRequired();

            builder.Entity<GameTeam>()
                .Property(x => x.Order)
                .IsRequired();

            builder.Entity<GameTeam>()
                .Property(x => x.RemainingFieldCount)
                .IsRequired();

            builder.Entity<GameTeam>()
                .Property(x => x.RemainingFieldCount)
                .IsRequired();
        }

        private static void InitGameTable(ModelBuilder builder)
        {
            builder.Entity<Game>()
                .HasKey(x => x.Id);

            builder.Entity<Game>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.UserId)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.TrapCount)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.ColumnsCount)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.RowsCount)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.TeamCount)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.Type)
                .IsRequired();
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
