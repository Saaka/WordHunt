using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class User : IdentityUser
    {
        public User(string userName) 
            : base(userName)
        {
        }
    }
}
