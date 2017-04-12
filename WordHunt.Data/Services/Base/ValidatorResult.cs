using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Data.Services.Base
{
    public class ValidatorResult
    {
        /// <summary>
        /// Valid validator result
        /// </summary>
        public ValidatorResult()
        {
            IsSuccess = true;
        }
        
        /// <summary>
        /// Invalid validator result
        /// </summary>
        /// <param name="error">Error message</param>
        public ValidatorResult(string error)
        {
            IsSuccess = false;
            ErrorMessage = error;
        }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
