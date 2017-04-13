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
    public interface IWordCreatorValidator
    {
        Task<ValidatorResult> ValidateRequest(WordCreateRequest request);
    }

    public class WordCreatorValidator : IWordCreatorValidator
    {
        private readonly AppDbContext context;

        public WordCreatorValidator(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ValidatorResult> ValidateRequest(WordCreateRequest request)
        {
            if (request.LanguageId <= 0)
                return new ValidatorResult("Must specify word language");
            if (string.IsNullOrEmpty(request.Value))
                return new ValidatorResult("Word must have a value");
            if (await WordValueExists(request.LanguageId, request.Value))
                return new ValidatorResult("Word already exists");

            return new ValidatorResult();
        }

        private async Task<bool> WordValueExists(long languageId, string value)
        {
            return await context.Words.Where(x => x.LanguageId == languageId && x.Value.ToLower() == value.ToLower()).AnyAsync();
        }
    }
}
