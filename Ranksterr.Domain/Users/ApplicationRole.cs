using Microsoft.AspNetCore.Identity;

namespace Ranksterr.Domain.Users;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole(string roleName) : base(roleName)
    {
        
    }

    public ApplicationRole()
    {
        
    }
}