using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Common;
using WordHunt.Interfaces.Words.DTO;

namespace WordHunt.Interfaces.Words.Result
{
    public class WordListGetResult : RequestResult
    {
        public WordListGetResult() : base() { }
        public WordListGetResult(string error) : base(error) { }

        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Word> WordList { get; set; }
    }
}
