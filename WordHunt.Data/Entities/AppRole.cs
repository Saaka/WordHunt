using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WordHunt.Data.Entities
{
    public class AppRole : IdentityRole<long>
    {
        public AppRole(string roleName) : base()
        {
            Name = roleName;
        }

        public AppRole() : base() { }

    }
}
