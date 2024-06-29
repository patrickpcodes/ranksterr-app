using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ranksterr.Api.Extensions;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Authentication;
using Ranksterr.Domain.Users;

namespace Ranksterr.Api.Controllers;

[ApiController]
[Route("api/")]
public class TokenController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;
    private IJwtService _jwtService;

    public TokenController( UserManager<ApplicationUser> userManager, IJwtService jwtService )
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost( "token" )]
    public async Task<IResult> GetToken( [FromBody] UserSeeding.UserCredentials userRecord )
    {
        var user = await UserSeeding.IsValidLoginInfo( _userManager, userRecord );

        //Todo separate code to handle unauthorized and Internal error
        if( user != null )
        {
            var token = await _jwtService.GenerateToken( _userManager, user );
            //await context.Response.WriteAsJsonAsync(new {token});
            return Results.Json( new { token } );
        }
        return Results.BadRequest();
        
    }

    [HttpGet( "users" )]
    public async Task<IResult> GetUsers()
    {
        var user = await GetUserFromClaimsPrincipal( HttpContext.User, _userManager );
        if( user == null )
        {
            return Results.NotFound();
        }
        var claims = HttpContext.User.Claims.Select( c => new ClaimRecord() { Type = c.Type, Value = c.Value } ).ToList();
        var roles = await _userManager.GetRolesAsync( user );

        var result = new { user, roles, claims };

        return Results.Json( result );
        
    }private static async Task<ApplicationUser?> GetUserFromClaimsPrincipal( ClaimsPrincipal claimsPrincipal, UserManager<ApplicationUser> userManager )
    {
        try
        {
            var userId = Guid.Parse( claimsPrincipal.Claims.First( c => c.Type.Equals( Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub ) )
                                                    .Value );
            var user = await userManager.Users.FirstOrDefaultAsync( c => c.Id.Equals( userId ) );
            if( user != null )
                return user;

            var username = claimsPrincipal.Claims.First( c => c.Type.Equals( Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name ) ).Value;
            user = await userManager.Users.FirstOrDefaultAsync( c => c.Email.Equals( username ) );
            if( user != null )
                return user;

            var userEmail = claimsPrincipal.Claims.First( c => c.Type.Equals( Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email ) ).Value;
            user = await userManager.Users.FirstOrDefaultAsync( c => c.Email.Equals( userEmail ) );
            if( user != null )
                return user;

            return null;
        }
        catch
        {
            return null;
        }
    }
}