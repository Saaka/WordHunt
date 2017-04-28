using System.Threading.Tasks;
using WordHunt.Base.Exceptions;
using WordHunt.Models.Categories.Access;

namespace WordHunt.Services.Categories
{
    public interface ICategoryProviderValidator
    {
        Task ValidateListGetModel(CategoryListGet model);
    }

    public class CategoryProviderValidator : ICategoryProviderValidator
    {
        public async Task ValidateListGetModel(CategoryListGet model)
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
