using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Common;
using WordHunt.DataInterfaces.Languages.DTO;

namespace WordHunt.DataInterfaces.Languages.Results
{
    public class LanguageListGetResult : RequestResult
    {
        public LanguageListGetResult() : base() { }
        public LanguageListGetResult(string error) : base(error) { }

        public IEnumerable<Language> Languages { get; set; }
    }
}
