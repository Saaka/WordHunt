using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Services.Base;
using WordHunt.DataInterfaces.Words;

namespace WordHunt.Data.Services.Words
{
    public interface IWordProviderValidator
    {
        ValidatorResult ValidateRequest(WordListRequest request);
    }

    public class WordProviderValidator : IWordProviderValidator
    {
        public ValidatorResult ValidateRequest(WordListRequest request)
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
