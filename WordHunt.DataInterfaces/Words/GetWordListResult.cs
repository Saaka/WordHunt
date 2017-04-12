using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Common;
using WordHunt.DataInterfaces.Words.DTO.Access;

namespace WordHunt.DataInterfaces.Words
{
    public class GetWordListResult : GetListResult
    {
        public GetWordListResult() : base()
        {
                
        }

        public GetWordListResult(string error) : base(error)
        {
        }

        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Word> WordList { get; set; }
    }
}
