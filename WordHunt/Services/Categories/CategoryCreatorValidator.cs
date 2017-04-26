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
        Task ValidateModel(CategoryCreate request);
    }

    public class CategoryCreatorValidator : ICategoryCreatorValidator
    {
        private readonly IAppDbContext context;

        public CategoryCreatorValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task ValidateModel(CategoryCreate request)
        {
            if (request.LanguageId <= 0)
                throw new ValidationFailedException("Category must have specified language");
            if (string.IsNullOrEmpty(request.Name))
                throw new ValidationFailedException("Category must have a name");
            if (await CategoryExists(request.LanguageId, request.Name))
                throw new ValidationFailedException("Category already exists");
        }

        private async Task<bool> CategoryExists(int languageId, string name)
        {
            return await context.Categories.Where(x => x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
