using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;

namespace WordHunt.Data.Identity
{
    public class AppUserStore : UserStore<AppUser, AppRole, AppDbContext, long>
    {
        public AppUserStore(AppDbContext context) : base(context) { }
    }

    public class AppRoleStore : RoleStore<AppRole, AppDbContext, long>
    {
        public AppRoleStore(AppDbContext context) : base(context) { }
    }
}
