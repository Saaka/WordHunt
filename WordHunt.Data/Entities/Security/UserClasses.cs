using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WordHunt.Data.Entities.Security
{
    public class UserRole : IdentityUserRole<long> { }
    public class UserClaim : IdentityUserClaim<long> { }
    public class UserLogin : IdentityUserLogin<long> { }
    public class UserToken : IdentityUserToken<long> { }
    public class RoleClaim : IdentityRoleClaim<long> { }
}