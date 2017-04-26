using System;
using System.Threading.Tasks;
using WordHunt.Services.Categories.Mapper;
using WordHunt.Data;
using WordHunt.Models.Categories.Creation;

namespace WordHunt.Services.Categories
{
    public interface ICategoryCreator
    {
        Task<CategoryCreateResult> CreateCategory(CategoryCreate request);
    }

    public class CategoryCreator : ICategoryCreator
    {
        private readonly IAppDbContext context;
        private readonly ICategoryCreatorValidator validator;
        private readonly ICategoryProvider categoryProvider;
        private readonly ICategoryMapper mapper;

        public CategoryCreator(IAppDbContext context,
            ICategoryCreatorValidator validator,
            ICategoryMapper mapper,
            ICategoryProvider categoryProvider)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
            this.categoryProvider = categoryProvider;
        }

        public async Task<CategoryCreateResult> CreateCategory(CategoryCreate request)
        {
            await validator.ValidateModel(request);

            var newCategory = mapper.MapCategory(request);
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();

            var categoryModel = await categoryProvider.GetCategory(newCategory.Id);
            
            return new CategoryCreateResult()
            {
                Category = categoryModel
            };
        }
    }
}
