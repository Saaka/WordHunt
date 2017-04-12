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
        private readonly AppDbContext context;
        private readonly IWordProviderValidator wordProviderValidator;

        public WordProvider(AppDbContext context,
            IWordProviderValidator wordProviderValidator)
        {
            this.context = context;
            this.wordProviderValidator = wordProviderValidator;
        }

        public async Task<GetWordListResult> GetWordList(WordListRequest request)
        {
            try
            {
                var validatorResult = wordProviderValidator.ValidateRequest(request);
                if (!validatorResult.IsSuccess)
                    return new GetWordListResult(validatorResult.ErrorMessage);

                var query = from word in context.Words

                            join cat in context.Categories
                                on word.CategoryId equals cat.Id into categories
                            from category in categories.DefaultIfEmpty()

                            where word.LanguageId == request.LanguageId
                                  && (request.CategoryId == null || word.CategoryId == request.CategoryId)
                                  && (request.Value == null || request.Value == "" || word.Value.Contains(request.Value) || request.Value.Contains(word.Value))

                            select new Word
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

                return new GetWordListResult()
                {
                    Count = count,
                    Page = request.Page,
                    PageCount = (int)Math.Ceiling((decimal)count / (decimal)request.PageSize),
                    WordList = words
                };
            }
            catch (Exception ex)
            {
                return new GetWordListResult(ex.Message);
            }
        }
    }
}
