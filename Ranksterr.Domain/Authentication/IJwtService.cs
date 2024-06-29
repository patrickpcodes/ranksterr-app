using Microsoft.AspNetCore.Identity;
using Ranksterr.Domain.Users;

namespace Ranksterr.Domain.Authentication;

public interface IJwtService
{
    Task<JwtToken> GenerateToken( UserManager<ApplicationUser> userManager, ApplicationUser user );
 
}