using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.Interfaces.Words.DTO;
using WordHunt.Interfaces.Words.Request;

namespace WordHunt.Services.Words.Mapper
{
    public class WordMapper : IWordMapper
    {
        public Data.Entities.Word MapCreateRequest(WordCreateRequest wordCreateRequest)
        {
            return new Data.Entities.Word()
            {
                CategoryId = wordCreateRequest.CategoryId,
                LanguageId = wordCreateRequest.LanguageId,
                Value = wordCreateRequest.Value
            };
        }

        public Data.Entities.Word MapWord(Data.Entities.Word word, WordUpdateRequest updateRequest)
        {
            word.CategoryId = updateRequest.CategoryId;
            word.Value = updateRequest.Value;

            return word;
        }
    }
}
