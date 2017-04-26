﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Services.Exceptions;
using WordHunt.Models.Words.Creation;

namespace WordHunt.Services.Words
{
    public interface IWordCreatorValidator
    {
        Task ValidateRequest(WordCreate model);
    }

    public class WordCreatorValidator : IWordCreatorValidator
    {
        private readonly IAppDbContext context;

        public WordCreatorValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task ValidateRequest(WordCreate model)
        {
            if (model.LanguageId <= 0)
                throw new ValidationFailedException("Must specify word language");
            if (string.IsNullOrEmpty(model.Value))
                throw new ValidationFailedException("Word must have a value");
            if (await WordValueExists(model.LanguageId, model.Value))
                throw new ValidationFailedException("Word already exists");
        }

        private async Task<bool> WordValueExists(long languageId, string value)
        {
            return await context.Words.Where(x => x.LanguageId == languageId && x.Value.ToLower() == value.ToLower()).AnyAsync();
        }
    }
}
