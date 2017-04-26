using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Services.Exceptions
{
    public class ValidationFailedException : ArgumentException
    {
        public ValidationFailedException() { }
        public ValidationFailedException(string message) : base(message) { }
    }
}
