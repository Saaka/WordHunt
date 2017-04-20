using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities.Security;

namespace WordHunt.Data.Entities
{
    public class User : IdentityUser<long, UserClaim, UserRole, UserLogin>
    {
    }
}
