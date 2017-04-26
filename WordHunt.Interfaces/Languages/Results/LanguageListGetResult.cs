using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Common;
using WordHunt.Interfaces.Languages.DTO;

namespace WordHunt.Interfaces.Languages.Results
{
    public class LanguageListGetResult : RequestResult
    {
        public LanguageListGetResult() : base() { }
        public LanguageListGetResult(string error) : base(error) { }

        public IEnumerable<Language> Languages { get; set; }
    }
}
