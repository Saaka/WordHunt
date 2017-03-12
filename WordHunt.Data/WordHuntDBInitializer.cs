using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Data
{
    public interface IWordHuntDBInitializer
    {
        Task InitDatabase();
    }

    public class WordHuntDBInitializer : IWordHuntDBInitializer
    {
        private WordHuntContext context;

        public WordHuntDBInitializer(WordHuntContext context)
        {
            this.context = context;
        }

        public async Task InitDatabase()
        {
            await context.Database.MigrateAsync();
        }
    }
}
