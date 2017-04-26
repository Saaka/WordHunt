using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Common;
using WordHunt.Interfaces.Words.DTO;

namespace WordHunt.Interfaces.Words.Result
{
    public class WordGetResult : RequestResult
    {
        public WordGetResult() : base() { }
        public WordGetResult(string error) : base(error) { }

        public Word Word { get; set; }
    }
}
