using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Categories.Request;
using WordHunt.Interfaces.Categories.Result;

namespace WordHunt.Interfaces.Categories
{
    public interface ICategoryCreator
    {
        Task<CategoryCreateResult> CreateCategory(CategoryCreateRequest request);
    }
}
