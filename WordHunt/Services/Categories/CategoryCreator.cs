using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Services.Categories.Mapper;
using WordHunt.Interfaces.Categories;
using WordHunt.Interfaces.Categories.Request;
using WordHunt.Interfaces.Categories.Result;
using WordHunt.Data;

namespace WordHunt.Services.Categories
{
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

        public async Task<CategoryCreateResult> CreateCategory(CategoryCreateRequest request)
        {
            try
            {
                var validatorResult = await validator.ValidateRequest(request);
                if (!validatorResult.IsSuccess)
                    return new CategoryCreateResult(validatorResult.Error);

                var newCategory = mapper.MapCreateRequest(request);
                await context.Categories.AddAsync(newCategory);
                await context.SaveChangesAsync();

                var getCategoryResult = await categoryProvider.GetCategory(newCategory.Id);

                if (!getCategoryResult.IsSuccess)
                    return new CategoryCreateResult(getCategoryResult.Error);

                return new CategoryCreateResult()
                {
                    Category = getCategoryResult.Category
                };
            }
            catch (Exception ex)
            {
                return new CategoryCreateResult(ex.Message);
            }
        }
    }
}
