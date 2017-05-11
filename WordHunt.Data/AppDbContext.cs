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
        public DbSet<GameTeam> GameTeams { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        public DbSet<GameField> GameFields { get; set; }
        public DbSet<GameClient> GameClients { get; set; }
        
        public AppDbContext(DbContextOptions options, IAppConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            InitWordTables(builder);

            InitGameTable(builder);
            InitGameTeamsTable(builder);
            InitGameStatusesTable(builder);
            InitGameFieldsTable(builder);
            InitGameClientsTable(builder);
        }

        private void InitGameClientsTable(ModelBuilder builder)
        {
            throw new NotImplementedException();
        }

        private void InitGameFieldsTable(ModelBuilder builder)
        {
            builder.Entity<GameField>()
                .HasKey(x => x.Id);

            builder.Entity<GameField>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Fields)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void InitGameStatusesTable(ModelBuilder builder)
        {
            builder.Entity<GameStatus>()
                .HasKey(x => x.Id);

            builder.Entity<GameStatus>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Statuses)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);
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
                .HasOne(x => x.Language)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .Property(x => x.BoardWidth)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.BoardHeight)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.TeamCount)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.Type)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.EndMode)
                .IsRequired();

            builder.Entity<Game>()
                .Property(x => x.CreationDate)
                .IsRequired();
        }

        private static void InitWordTables(ModelBuilder builder)
        {
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
