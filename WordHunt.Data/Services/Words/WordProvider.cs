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

        public async Task<IEnumerable<Word>> GetWordList(WordListFilter filter)
        {
            var query = from word in context.Words.AsQueryable()

                        join c in context.Categories.AsQueryable()
                            on word.CategoryId equals c.Id into cv
                        from category in cv.DefaultIfEmpty()

                        where word.LanguageId == filter.LanguageId
                              && (filter.CategoryId == null || word.CategoryId == filter.CategoryId)
                              && (filter.Value == null || filter.Value == "" || word.Value.Contains(filter.Value) || filter.Value.Contains(word.Value))

                        select new Word
                        {
                            Id = word.Id,
                            Value = word.Value,
                            CategoryId = word.CategoryId,
                            Category = category.Name
                        };

            if (filter.OrderByDesc)
                query = query.OrderByDescending(x => x.Value);
            else
                query = query.OrderBy(x => x.Value);

            if (filter.PageSize > 0)
            {
                var skipValue = (filter.Page - 1) * filter.PageSize;
                query = query.Skip(skipValue).Take(filter.PageSize);
            }

            var words = await query.ToListAsync();

            return words;
        }
    }
}
