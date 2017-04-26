using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Common
{
    public class RequestResult
    {
        public RequestResult(string error)
        {
            IsSuccess = false;
            this.Error = error;
        }

        public RequestResult()
        {
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}
