using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.Request
{
    public class WordCreateRequest
    {
        public string Value { get; set; }
        public long LanguageId { get; set; }
        public long? CategoryId { get; set; }
    }
}
