using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Models.Categories.Modification;
using WordHunt.Services.Exceptions;

namespace WordHunt.Services.Categories.Mapper
{
    public interface ICategoryUpdaterValidator
    {
        Task ValidateUpdateModel(CategoryUpdate model);
    }

    public class CategoryUpdaterValidator : ICategoryUpdaterValidator
    {
        private readonly IAppDbContext context;

        public CategoryUpdaterValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task ValidateUpdateModel(CategoryUpdate model)
        {
            if (model == null)
                throw new ValidationFailedException("No data provided to perform update");
            if (model.Id <= 0)
                throw new ValidationFailedException("Must specify word id");
            if (string.IsNullOrEmpty(model.Name))
                throw new ValidationFailedException("Word must have a value");
            if (await WordValueExists(model.Id, model.Id, model.Name))
                throw new ValidationFailedException("Word already exists");
        }

        private async Task<bool> WordValueExists(long languageId, long categoryId, string name)
        {
            return await context.Categories.Where(x => x.Id != categoryId && x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
