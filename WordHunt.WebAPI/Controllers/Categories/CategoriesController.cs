using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordHunt.Config.Auth;
using WordHunt.Models.Categories.Access;
using WordHunt.Models.Categories.Creation;
using WordHunt.Models.Categories.Modification;
using WordHunt.Services.Categories;

namespace WordHunt.WebAPI.Controllers.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryProvider categoryProvider;
        private readonly ICategoryCreator categoryCreator;
        private readonly ICategoryUpdater categoryUpdater;

        public CategoriesController(ICategoryCreator categoryCreator,
            ICategoryUpdater categoryUpdater,
            ICategoryProvider categoryProvider)
        {
            this.categoryCreator = categoryCreator;
            this.categoryProvider = categoryProvider;
            this.categoryUpdater = categoryUpdater;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("list")]
        public async Task<CategoryListGetResult> GetCategoryList([FromBody]CategoryListGet request)
        {
            return await categoryProvider.GetCategoryList(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("add")]
        public async Task<CategoryCreateResult> CreateCategory([FromBody]CategoryCreate request)
        {
            return await categoryCreator.CreateCategory(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("update")]
        public async Task<CategoryUpdateResult> UpdateCategory([FromBody]CategoryUpdate request)
        {
            return await categoryUpdater.UpdateCategory(request);
        }
    }
}