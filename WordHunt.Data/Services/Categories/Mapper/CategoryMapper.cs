using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Categories.Request;

namespace WordHunt.Data.Services.Categories.Mapper
{
    public interface ICategoryMapper
    {
        Entities.Category MapCreateRequest(CategoryCreateRequest request);
        Entities.Category MapCategory(Entities.Category category, CategoryUpdateRequest request);
    }
    class CategoryMapper : ICategoryMapper
    {
        public Entities.Category MapCreateRequest(CategoryCreateRequest request)
        {
            return new Entities.Category()
            {
                LanguageId = request.LanguageId,
                Name = request.Name
            };
        }

        public Entities.Category MapCategory(Entities.Category category, CategoryUpdateRequest request)
        {
            category.Name = request.Name;

            return category;
        }
    }
}
