using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WordHunt.Config;

namespace WordHunt.Data
{
    public class WordHuntContext : IdentityDbContext
    {
        private IAppConfiguration config;

        public WordHuntContext(DbContextOptions options, IAppConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlServer(config.DbConnectionString, 
                ob => ob.MigrationsHistoryTable("WordHuntMigrations"));

        }
    }
}
