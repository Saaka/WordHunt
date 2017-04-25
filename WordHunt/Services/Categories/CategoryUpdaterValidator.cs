using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Services.Base;
using WordHunt.DataInterfaces.Categories.Request;
using WordHunt.Data;

namespace WordHunt.Services.Categories.Mapper
{
    public interface ICategoryUpdaterValidator
    {
        Task<ValidatorResult> ValidateRequest(CategoryUpdateRequest request);
    }

    public class CategoryUpdaterValidator : ICategoryUpdaterValidator
    {
        private readonly IAppDbContext context;

        public CategoryUpdaterValidator(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<ValidatorResult> ValidateRequest(CategoryUpdateRequest request)
        {
            if (request.Id <= 0)
                return new ValidatorResult("Must specify word id");
            if (string.IsNullOrEmpty(request.Name))
                return new ValidatorResult("Word must have a value");
            if (await WordValueExists(request.Id, request.Id, request.Name))
                return new ValidatorResult("Word already exists");

            return new ValidatorResult();
        }

        private async Task<bool> WordValueExists(long languageId, long categoryId, string name)
        {
            return await context.Categories.Where(x => x.Id != categoryId && x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
