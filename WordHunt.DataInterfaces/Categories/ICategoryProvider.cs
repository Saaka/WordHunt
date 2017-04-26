using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Categories.Request;
using WordHunt.Interfaces.Categories.Result;

namespace WordHunt.Interfaces.Categories
{
    public interface ICategoryProvider
    {
        Task<CategoryListResult> GetCategoryList(CategoryListRequest request);
        Task<CategoryGetResult> GetCategory(int categoryId);
    }
}
