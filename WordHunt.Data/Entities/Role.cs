using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WordHunt.Data.Entities
{
    public class Role : IdentityRole<long>
    {
        public Role(string roleName) : base()
        {
            Name = roleName;
        }

        public Role() : base() { }

    }
}
