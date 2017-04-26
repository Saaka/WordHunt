using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Categories.Request;

namespace WordHunt.Services.Categories.Mapper
{
    public interface ICategoryMapper
    {
        Data.Entities.Category MapCreateRequest(CategoryCreateRequest request);
        Data.Entities.Category MapCategory(Data.Entities.Category category, CategoryUpdateRequest request);
    }
    class CategoryMapper : ICategoryMapper
    {
        public Data.Entities.Category MapCreateRequest(CategoryCreateRequest request)
        {
            return new Data.Entities.Category()
            {
                LanguageId = request.LanguageId,
                Name = request.Name
            };
        }

        public Data.Entities.Category MapCategory(Data.Entities.Category category, CategoryUpdateRequest request)
        {
            category.Name = request.Name;

            return category;
        }
    }
}
