using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Models.Languages;

namespace WordHunt.Services.Languages
{
    public interface ILanguageProvider
    {
        Task<IEnumerable<LanguageModel>> GetLanguageList();
    }

    public class LanguageProvider : ILanguageProvider
    {
        private readonly IAppDbContext context;

        public LanguageProvider(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<LanguageModel>> GetLanguageList()
        {
            var query = from language in context.Languages
                        orderby language.Name
                        select new LanguageModel
                        {
                            Id = language.Id,
                            Code = language.Code,
                            Name = language.Name
                        };

            var languages = await query.ToListAsync();

            return languages;
        }
    }
}
