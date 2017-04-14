using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Categories.Request;
using WordHunt.DataInterfaces.Categories.Result;

namespace WordHunt.DataInterfaces.Categories
{
    public interface ICategoryUpdater
    {
        Task<CategoryUpdateResult> CreateCategory(CategoryUpdateRequest request);
    }
}
