using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Services.Categories.Mapper;
using WordHunt.DataInterfaces.Categories;
using WordHunt.DataInterfaces.Categories.Request;
using WordHunt.DataInterfaces.Categories.Result;

namespace WordHunt.Data.Services.Categories
{
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

        public async Task<CategoryUpdateResult> UpdateCategory(CategoryUpdateRequest request)
        {
            try
            {
                var validatorResult = await validator.ValidateRequest(request);
                if (!validatorResult.IsSuccess)
                    return new CategoryUpdateResult(validatorResult.Error);

                var toUpdate = await context.Categories.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (toUpdate == null)
                    return new CategoryUpdateResult($"Can't fine category with id {request.Id}");

                toUpdate = mapper.MapCategory(toUpdate, request);

                await context.SaveChangesAsync();

                var getCategoryResult = await categoryProvider.GetCategory(request.Id);
                if (!getCategoryResult.IsSuccess)
                    return new CategoryUpdateResult(getCategoryResult.Error);

                return new CategoryUpdateResult()
                {
                    Category = getCategoryResult.Category
                };
            }
            catch (Exception ex)
            {
                return new CategoryUpdateResult(ex.Message);
            }
        }
    }
}
