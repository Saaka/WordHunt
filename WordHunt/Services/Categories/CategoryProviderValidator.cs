using System.Threading.Tasks;
using WordHunt.Models.Categories.Access;
using WordHunt.Services.Exceptions;

namespace WordHunt.Services.Categories
{
    public interface ICategoryProviderValidator
    {
        Task ValidateRequest(CategoryListGet model);
    }

    public class CategoryProviderValidator : ICategoryProviderValidator
    {
        public async Task ValidateRequest(CategoryListGet model)
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
