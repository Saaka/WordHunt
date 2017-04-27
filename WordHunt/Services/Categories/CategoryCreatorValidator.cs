using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Services.Exceptions;
using WordHunt.Models.Categories.Creation;

namespace WordHunt.Services.Categories
{
    public interface ICategoryCreatorValidator
    {
        Task ValidateCreateModel(CategoryCreate model);
    }

    public class CategoryCreatorValidator : ICategoryCreatorValidator
    {
        private readonly IAppDbContext context;

        public CategoryCreatorValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task ValidateCreateModel(CategoryCreate model)
        {
            if (model== null)
                throw new ValidationFailedException("No data provided to create category");
            if (model.LanguageId <= 0)
                throw new ValidationFailedException("Category must have specified language");
            if (string.IsNullOrEmpty(model.Name))
                throw new ValidationFailedException("Category must have a name");
            if (await CategoryExists(model.LanguageId, model.Name))
                throw new ValidationFailedException("Category already exists");
        }

        private async Task<bool> CategoryExists(int languageId, string name)
        {
            return await context.Categories.Where(x => x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
