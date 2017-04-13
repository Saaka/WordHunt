using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Common;
using WordHunt.DataInterfaces.Words.DTO;

namespace WordHunt.DataInterfaces.Words.Result
{
    public class CreateWordResult : RequestResult
    {
        public CreateWordResult() : base() { }
        public CreateWordResult(string error) : base(error) { }

        public Word Word { get; set; }
    }
}
