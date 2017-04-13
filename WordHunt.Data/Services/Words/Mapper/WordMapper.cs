using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.DataInterfaces.Words.DTO;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.Data.Services.Words.Mapper
{
    public class WordMapper : IWordMapper
    {
        public Entities.Word MapCreateRequest(WordCreateRequest wordCreateRequest)
        {
            return new Entities.Word()
            {
                CategoryId = wordCreateRequest.CategoryId,
                LanguageId = wordCreateRequest.LanguageId,
                Value = wordCreateRequest.Value
            };
        }
    }
}
