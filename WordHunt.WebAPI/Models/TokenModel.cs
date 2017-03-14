using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordHunt.WebAPI.Models
{
    public class TokenModel
    {
        public TokenModel(string token,
            DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
