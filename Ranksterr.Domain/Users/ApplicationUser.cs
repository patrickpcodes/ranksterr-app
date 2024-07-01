using Microsoft.AspNetCore.Identity;
using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Domain.Users;

public class ApplicationUser :  IdentityUser<Guid>
{
}