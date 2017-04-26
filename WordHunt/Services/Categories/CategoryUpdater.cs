using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WordHunt.Services.Categories.Mapper;
using WordHunt.Data;
using WordHunt.Models.Categories.Modification;

namespace WordHunt.Services.Categories
{
    public interface ICategoryUpdater
    {
        Task<CategoryUpdateResult> UpdateCategory(CategoryUpdate model);
    }

    public class CategoryUpdater : ICategoryUpdater
    {
        private readonly IAppDbContext context;
        private readonly ICategoryUpdaterValidator validator;
        private readonly ICategoryMapper mapper;
        private readonly ICategoryProvider categoryProvider;

        public CategoryUpdater(IAppDbContext context,
            ICategoryUpdaterValidator validator,
            ICategoryMapper mapper,
            ICategoryProvider categoryProvider)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
            this.categoryProvider = categoryProvider;
        }

        public async Task<CategoryUpdateResult> UpdateCategory(CategoryUpdate model)
        {
            await validator.ValidateRequest(model);

            var toUpdate = await context.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (toUpdate == null)
                throw new ArgumentException($"Can't fine category with id {model.Id}");

            toUpdate = mapper.MapCategory(toUpdate, model);

            await context.SaveChangesAsync();

            var categoryModel = await categoryProvider.GetCategory(model.Id);

            return new CategoryUpdateResult()
            {
                Category = categoryModel
            };
        }
    }
}
