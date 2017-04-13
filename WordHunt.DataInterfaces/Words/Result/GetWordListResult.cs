using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Common;
using WordHunt.DataInterfaces.Words.DTO;

namespace WordHunt.DataInterfaces.Words.Result
{
    public class GetWordListResult : RequestResult
    {
        public GetWordListResult() : base() { }
        public GetWordListResult(string error) : base(error) { }

        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Word> WordList { get; set; }
    }
}
