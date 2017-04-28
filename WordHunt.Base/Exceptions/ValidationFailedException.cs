using System;

namespace WordHunt.Base.Exceptions
{
    public class ValidationFailedException : ArgumentException
    {
        public ValidationFailedException() { }
        public ValidationFailedException(string message) : base(message) { }
    }
}
