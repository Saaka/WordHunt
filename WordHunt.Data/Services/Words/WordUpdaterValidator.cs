using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Services.Base;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.Data.Services.Words
{
    public interface IWordUpdaterValidator
    {
        Task<ValidatorResult> ValidateRequest(WordUpdateRequest request);
    }

    public class WordUpdaterValidator : IWordUpdaterValidator
    {
        private readonly IAppDbContext context;

        public WordUpdaterValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<ValidatorResult> ValidateRequest(WordUpdateRequest request)
        {
            if (request.Id <= 0)
                return new ValidatorResult("Must specify word id");
            if (string.IsNullOrEmpty(request.Value))
                return new ValidatorResult("Word must have a value");
            if (await WordValueExists(request.Id, request.Id, request.Value))
                return new ValidatorResult("Word already exists");

            return new ValidatorResult();
        }

        private async Task<bool> WordValueExists(long languageId, long wordId, string value)
        {
            return await context.Words.Where(x => x.Id != wordId && x.LanguageId == languageId && x.Value.ToLower() == value.ToLower()).AnyAsync();
        }
    }
}
