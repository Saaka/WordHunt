using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Services.Base;
using WordHunt.DataInterfaces.Words;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.Services.Words
{
    public interface IWordProviderValidator
    {
        ValidatorResult ValidateRequest(WordListGetRequest request);
    }

    public class WordProviderValidator : IWordProviderValidator
    {
        public ValidatorResult ValidateRequest(WordListGetRequest request)
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
