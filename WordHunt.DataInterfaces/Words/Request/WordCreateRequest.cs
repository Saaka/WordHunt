using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.Request
{
    public class WordCreateRequest
    {
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
