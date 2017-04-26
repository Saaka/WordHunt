using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Interfaces.Languages;
using WordHunt.Interfaces.Languages.DTO;
using WordHunt.Interfaces.Languages.Results;

namespace WordHunt.Services.Languages
{
    public class LanguageProvider : ILanguageProvider
    {
        private readonly IAppDbContext context;

        public LanguageProvider(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<LanguageListGetResult> GetLanguageList()
        {
            try
            {
                var query = from language in context.Languages
                            orderby language.Name
                            select new Language
                            {
                                Id = language.Id,
                                Code = language.Code,
                                Name = language.Name
                            };

                var languages = await query.ToListAsync();

                return new LanguageListGetResult()
                {
                    Languages = languages
                };
            }
            catch(Exception ex)
            {
                return new LanguageListGetResult(ex.Message);
            }
        }
    }
}
