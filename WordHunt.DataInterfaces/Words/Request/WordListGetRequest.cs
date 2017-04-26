using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Words.Request
{
    public class WordListGetRequest
    {
        public int LanguageId { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool OrderByDesc { get; set; }
        public string Value { get; set; }
    }
}
