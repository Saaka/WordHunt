using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Config
{
    public interface IAuthConfiguration
    {
        string TokenKey { get; }
        string Issuer { get; }
        string Audience { get; }
        int TokenValidInMinutes { get; }
    }
}
