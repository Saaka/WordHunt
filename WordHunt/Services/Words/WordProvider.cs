using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Models.Words;
using WordHunt.Models.Words.Access;

namespace WordHunt.Services.Words
{
    public interface IWordProvider
    {
        Task<WordListGetResult> GetWordList(WordListGet request);
        Task<WordModel> GetWord(int wordId);
    }

    public class WordProvider : IWordProvider
    {
        private readonly IAppDbContext context;
        private readonly IWordProviderValidator wordProviderValidator;

        public WordProvider(IAppDbContext context,
            IWordProviderValidator wordProviderValidator)
        {
            this.context = context;
            this.wordProviderValidator = wordProviderValidator;
        }

        public async Task<WordModel> GetWord(int wordId)
        {
            var query = from word in context.Words
                        join cat in context.Categories
                            on word.CategoryId equals cat.Id into categories
                        from category in categories.DefaultIfEmpty()

                        where word.Id == wordId

                        select new WordModel
                        {
                            Id = word.Id,
                            Value = word.Value,
                            CategoryId = word.CategoryId,
                            Category = category != null ? category.Name : null
                        };

            var wordModel = await query.SingleOrDefaultAsync();

            return wordModel;
        }

        public async Task<WordListGetResult> GetWordList(WordListGet request)
        {
            await wordProviderValidator.ValidateListGetModel(request);

            var query = from word in context.Words

                        join cat in context.Categories
                            on word.CategoryId equals cat.Id into categories
                        from category in categories.DefaultIfEmpty()

                        where word.LanguageId == request.LanguageId
                              && (request.CategoryId == null || word.CategoryId == request.CategoryId || (request.CategoryId == 0 && word.CategoryId == null))
                              && (request.Value == null || request.Value == "" || word.Value.Contains(request.Value) || request.Value.Contains(word.Value))

                        select new WordModel
                        {
                            Id = word.Id,
                            Value = word.Value,
                            CategoryId = word.CategoryId,
                            Category = category != null ? category.Name : null
                        };

            var count = await query.CountAsync();

            if (request.OrderByDesc)
                query = query.OrderByDescending(x => x.Value);
            else
                query = query.OrderBy(x => x.Value);

            if (request.PageSize > 0)
            {
                var skipValue = (request.Page - 1) * request.PageSize;
                query = query.Skip(skipValue).Take(request.PageSize);
            }


            var words = await query.ToListAsync();

            return new WordListGetResult()
            {
                Count = count,
                Page = request.Page,
                PageCount = (int)Math.Ceiling((decimal)count / (decimal)request.PageSize),
                WordList = words
            };
        }
    }
}
