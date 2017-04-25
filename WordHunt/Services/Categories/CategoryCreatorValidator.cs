using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Services.Base;
using WordHunt.DataInterfaces.Categories.Request;
using WordHunt.Data;

namespace WordHunt.Services.Categories
{
    public interface ICategoryCreatorValidator
    {
        Task<ValidatorResult> ValidateRequest(CategoryCreateRequest request);
    }

    public class CategoryCreatorValidator : ICategoryCreatorValidator
    {
        private readonly IAppDbContext context;

        public CategoryCreatorValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<ValidatorResult> ValidateRequest(CategoryCreateRequest request)
        {
            if (request.LanguageId <= 0)
                return new ValidatorResult("Category must have specified language");
            if (string.IsNullOrEmpty(request.Name))
                return new ValidatorResult("Category must have a name");
            if (await CategoryExists(request.LanguageId, request.Name))
                return new ValidatorResult("Category already exists");

            return new ValidatorResult();
        }

        private async Task<bool> CategoryExists(int languageId, string name)
        {
            return await context.Categories.Where(x => x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
