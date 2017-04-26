using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Common;
using WordHunt.Interfaces.Words.DTO;

namespace WordHunt.Interfaces.Words.Result
{
    public class WordCreateResult : RequestResult
    {
        public WordCreateResult() : base() { }
        public WordCreateResult(string error) : base(error) { }

        public Word Word { get; set; }
    }
}
