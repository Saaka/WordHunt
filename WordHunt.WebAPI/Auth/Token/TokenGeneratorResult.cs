using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.WebAPI.Models;

namespace WordHunt.WebAPI.Auth.Token
{
    public enum TokenGeneratorResultStatus
    {
        Success,
        UserNotFound,
        InvalidPassword
    }

    public class TokenGeneratorResult
    {
        public TokenModel Token { get; set; }
        public TokenGeneratorResultStatus ResultStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
