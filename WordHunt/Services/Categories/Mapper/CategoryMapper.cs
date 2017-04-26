using WordHunt.Data.Entities;
using WordHunt.Models.Categories.Creation;
using WordHunt.Models.Categories.Modification;

namespace WordHunt.Services.Categories.Mapper
{
    public interface ICategoryMapper
    {
        Category MapCategory(CategoryCreate model);
        Category MapCategory(Data.Entities.Category category, CategoryUpdate model);
    }
    class CategoryMapper : ICategoryMapper
    {
        public Category MapCategory(CategoryCreate model)
        {
            return new Category()
            {
                LanguageId = model.LanguageId,
                Name = model.Name
            };
        }

        public Category MapCategory(Category category, CategoryUpdate model)
        {
            category.Name = model.Name;

            return category;
        }
    }
}
