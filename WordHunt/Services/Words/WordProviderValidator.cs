using System.Threading.Tasks;
using WordHunt.Models.Words.Access;
using WordHunt.Services.Exceptions;

namespace WordHunt.Services.Words
{
    public interface IWordProviderValidator
    {
        Task Validate(WordListGet model);
    }

    public class WordProviderValidator : IWordProviderValidator
    {
        public async Task Validate(WordListGet model)
        {
            if (model.LanguageId == 0)
                throw new ValidationFailedException("Must specify language");
            if (model.Page <= 0)
                throw new ValidationFailedException("Page must be greater than zero");
            if (model.PageSize <= 0)
                throw new ValidationFailedException("Page size must be greater than zero");
        }
    }
}
