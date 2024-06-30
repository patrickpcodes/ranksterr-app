using Microsoft.AspNetCore.Identity;
using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Domain.Users;

public class ApplicationUser :  IdentityUser<Guid>
{
    // public FirstName FirstName { get; set; }
    //
    // public LastName LastName { get; set; }
    //
    // public string SuperPrivateField { get; set; }
}

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole(string roleName) : base(roleName)
    {
        
    }

    public ApplicationRole()
    {
        
    }
}