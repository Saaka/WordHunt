using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Config
{
    public interface ISeedConfiguration
    {
        string AdminEmail { get; }
        string AdminName { get; }
        string AdminPassword { get; }

        string UserEmail { get; }
        string UserName { get; }
        string UserPassword { get; }
    }
}
