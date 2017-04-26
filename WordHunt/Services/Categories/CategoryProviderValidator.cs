using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Services.Base;
using WordHunt.Interfaces.Categories.Request;

namespace WordHunt.Services.Categories
{
    public interface ICategoryProviderValidator
    {
        ValidatorResult ValidateRequest(CategoryListRequest request);
    }

    public class CategoryProviderValidator : ICategoryProviderValidator
    {
        public ValidatorResult ValidateRequest(CategoryListRequest request)
        {
            if (request.LanguageId == 0)
                return new ValidatorResult("Must specify language");
            if (request.Page <= 0)
                return new ValidatorResult("Page must be greater than zero");
            if (request.PageSize <= 0)
                return new ValidatorResult("Page size must be greater than zero");

            return new ValidatorResult();
        }
    }
}
