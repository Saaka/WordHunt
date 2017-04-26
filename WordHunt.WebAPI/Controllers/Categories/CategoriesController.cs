using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordHunt.Config.Auth;
using WordHunt.Interfaces.Categories;
using WordHunt.Interfaces.Categories.Request;
using WordHunt.Interfaces.Categories.Result;

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
        public async Task<CategoryListResult> GetCategoryList([FromBody]CategoryListRequest request)
        {
            return await categoryProvider.GetCategoryList(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("add")]
        public async Task<CategoryCreateResult> CreateCategory([FromBody]CategoryCreateRequest request)
        {
            return await categoryCreator.CreateCategory(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("update")]
        public async Task<CategoryUpdateResult> UpdateCategory([FromBody]CategoryUpdateRequest request)
        {
            return await categoryUpdater.UpdateCategory(request);
        }
    }
}