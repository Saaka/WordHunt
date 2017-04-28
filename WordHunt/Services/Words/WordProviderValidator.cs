using System.Threading.Tasks;
using WordHunt.Base.Exceptions;
using WordHunt.Models.Words.Access;

namespace WordHunt.Services.Words
{
    public interface IWordProviderValidator
    {
        Task ValidateListGetModel(WordListGet model);
    }

    public class WordProviderValidator : IWordProviderValidator
    {
        public async Task ValidateListGetModel(WordListGet model)
        {
            if (model == null)
                throw new ValidationFailedException("No query data provided");
            if (model.LanguageId == 0)
                throw new ValidationFailedException("Must specify language");
            if (model.Page <= 0)
                throw new ValidationFailedException("Page must be greater than zero");
            if (model.PageSize <= 0)
                throw new ValidationFailedException("Page size must be greater than zero");
        }
    }
}
