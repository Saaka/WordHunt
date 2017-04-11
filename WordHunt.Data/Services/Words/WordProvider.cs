using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words;
using WordHunt.DataInterfaces.Words.DTO.Access;

namespace WordHunt.Data.Services.Words
{
    public class WordProvider : IWordProvider
    {
        private AppDbContext context;

        public WordProvider(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Word>> GetWords(string languageCode, long categoryId = 0)
        {
            var query = from word in context.Words.AsQueryable()

                        join lang in context.Languages.AsQueryable()
                            on word.LanguageId equals lang.Id

                        join c in context.Categories.AsQueryable()
                            on word.CategoryId equals c.Id into cv
                        from category in cv.DefaultIfEmpty()

                        where lang.Code == languageCode
                        select new Word
                        {
                            Id = word.Id,
                            Name = word.Value,
                            CategoryId = word.CategoryId,
                            Category = category.Name
                        };

            var words = await query.ToListAsync();

            return words;
        }
    }
}
