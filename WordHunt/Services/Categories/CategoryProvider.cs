using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Models.Categories;
using WordHunt.Models.Categories.Access;

namespace WordHunt.Services.Categories
{
    public interface ICategoryProvider
    {
        Task<CategoryListGetResult> GetCategoryList(CategoryListGet request);
        Task<CategoryModel> GetCategory(int categoryId);
    }

    public class CategoryProvider : ICategoryProvider
    {
        private readonly IAppDbContext context;
        private readonly ICategoryProviderValidator validator;

        public CategoryProvider(IAppDbContext context,
            ICategoryProviderValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public async Task<CategoryModel> GetCategory(int categoryId)
        {
            var query = from cat in context.Categories
                        where cat.Id == categoryId
                        select new CategoryModel
                        {
                            Id = cat.Id,
                            Name = cat.Name
                        };

            var categoryResult = await query.SingleOrDefaultAsync();
            if (categoryResult == null)
                throw new ArgumentException($"Can't find category with id {categoryId}");

            return categoryResult;
        }

        public async Task<CategoryListGetResult> GetCategoryList(CategoryListGet model)
        {
            var validatorResult = validator.ValidateRequest(model);

            var query = from cat in context.Categories

                        where cat.LanguageId == model.LanguageId
                              && (model.Name == null || model.Name == "" || cat.Name.Contains(model.Name) || model.Name.Contains(cat.Name))

                        select new CategoryModel
                        {
                            Id = cat.Id,
                            Name = cat.Name
                        };

            var count = await query.CountAsync();

            if (model.OrderByDesc)
                query = query.OrderByDescending(x => x.Name);
            else
                query = query.OrderBy(x => x.Name);

            if (model.PageSize > 0)
            {
                var skipValue = (model.Page - 1) * model.PageSize;
                query = query.Skip(skipValue).Take(model.PageSize);
            }

            var categories = await query.ToListAsync();

            return new CategoryListGetResult()
            {
                Count = count,
                Page = model.Page,
                PageCount = (int)Math.Ceiling((decimal)count / (decimal)model.PageSize),
                CategoryList = categories
            };
        }
    }
}
