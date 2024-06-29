using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ranksterr.Domain.Users;

namespace Ranksterr.Infrastructure;

public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public UserDbContext( DbContextOptions<UserDbContext> options )
        : base( options )
    {
    }
}