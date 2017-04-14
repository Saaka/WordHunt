using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Categories;
using WordHunt.DataInterfaces.Categories.DTO;
using WordHunt.DataInterfaces.Categories.Request;
using WordHunt.DataInterfaces.Categories.Result;

namespace WordHunt.Data.Services.Categories
{
    public class CategoryProvider : ICategoryProvider
    {
        private readonly AppDbContext context;
        private readonly ICategoryProviderValidator validator;

        public CategoryProvider(AppDbContext context,
            ICategoryProviderValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public async Task<CategoryGetResult> GetCategory(long categoryId)
        {
            try
            {
                var query = from cat in context.Categories
                            where cat.Id == categoryId
                            select new Category
                            {
                                Id = cat.Id,
                                Name = cat.Name
                            };

                var categoryResult = await query.SingleOrDefaultAsync();
                if (categoryResult == null)
                    return new CategoryGetResult($"Can't find category with id {categoryId}");

                return new CategoryGetResult()
                {
                    Category = categoryResult
                };
            }
            catch (Exception ex)
            {
                return new CategoryGetResult(ex.Message);
            }
        }

        public async Task<CategoryListResult> GetCategoryList(CategoryListRequest request)
        {
            try
            {
                var validatorResult = validator.ValidateRequest(request);
                if (!validatorResult.IsSuccess)
                    return new CategoryListResult(validatorResult.Error);

                var query = from cat in context.Categories

                            where cat.LanguageId == request.LanguageId
                                  && (request.Name == null || request.Name == "" || cat.Name.Contains(request.Name) || request.Name.Contains(cat.Name))

                            select new Category
                            {
                                Id = cat.Id,
                                Name = cat.Name
                            };

                var count = await query.CountAsync();

                if (request.OrderByDesc)
                    query = query.OrderByDescending(x => x.Name);
                else
                    query = query.OrderBy(x => x.Name);

                if (request.PageSize > 0)
                {
                    var skipValue = (request.Page - 1) * request.PageSize;
                    query = query.Skip(skipValue).Take(request.PageSize);
                }

                var categories = await query.ToListAsync();

                return new CategoryListResult()
                {
                    Count = count,
                    Page = request.Page,
                    PageCount = (int)Math.Ceiling((decimal)count / (decimal)request.PageSize),
                    CategoryList = categories
                };
            }
            catch (Exception ex)
            {
                return new CategoryListResult(ex.Message);
            }
        }
    }
}
