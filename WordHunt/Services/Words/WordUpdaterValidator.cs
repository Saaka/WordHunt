using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Base.Exceptions;
using WordHunt.Data;
using WordHunt.Models.Words.Modification;

namespace WordHunt.Services.Words
{
    public interface IWordUpdaterValidator
    {
        Task ValidateUpdateModel(WordUpdate model);
    }

    public class WordUpdaterValidator : IWordUpdaterValidator
    {
        private readonly IAppDbContext context;

        public WordUpdaterValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task ValidateUpdateModel(WordUpdate model)
        {
            if (model == null)
                throw new ValidationFailedException("No data provided to perform update");
            if (model.Id <= 0)
                throw new ValidationFailedException("Must specify word id");
            if (string.IsNullOrEmpty(model.Value))
                throw new ValidationFailedException("Word must have a value");
            if (await WordValueExists(model.Id, model.Id, model.Value))
                throw new ValidationFailedException("Word already exists");
        }

        private async Task<bool> WordValueExists(long languageId, long wordId, string value)
        {
            return await context.Words.Where(x => x.Id != wordId && x.LanguageId == languageId && x.Value.ToLower() == value.ToLower()).AnyAsync();
        }
    }
}
