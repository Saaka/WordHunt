using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.Request
{
    public class WordUpdateRequest
    {
        public int WordId { get; set; }
        public string Value { get; set; }
        public long? CategoryId { get; set; }
        public long LanguageId { get; set; }
    }
}
