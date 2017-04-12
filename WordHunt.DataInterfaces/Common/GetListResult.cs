using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Common
{
    public class GetListResult
    {
        public GetListResult(string error)
        {
            IsSuccess = false;
            this.Error = error;
        }

        public GetListResult()
        {
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}
