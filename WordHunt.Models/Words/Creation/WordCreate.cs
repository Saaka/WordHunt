using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Models.Words.Creation
{
    public class WordCreate
    {
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }

    public class WordCreateResult
    {
        public int WordId { get; set; }
    }
}
