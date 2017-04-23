using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WordHunt.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string roleName) : base()
        {
            Name = roleName;
        }

        public Role() : base() { }

    }
}
