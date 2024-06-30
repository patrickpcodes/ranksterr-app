using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Authentication;
using Ranksterr.Domain.Users;

namespace Ranksterr.Application.Authentication;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService( ISettingsService<JwtSettings> jwtSettings )
    {
        _jwtSettings = jwtSettings.GetSettings();
    }

    public async Task<JwtToken> GenerateToken( UserManager<ApplicationUser> userManager, ApplicationUser user )
    {
        var claimList = await userManager.GetClaimsAsync( user );
        var roleList = ( await userManager.GetRolesAsync( user ) ).Select( c => new Claim( ClaimTypes.Role, c ) );
        var expirationDate = new DateTimeOffset( DateTime.Now.AddMinutes( _jwtSettings.ExpiryInMinutes ) );

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Nbf, new DateTimeOffset( DateTime.Now ).ToUnixTimeSeconds().ToString()),
            new(JwtRegisteredClaimNames.Exp, expirationDate.ToUnixTimeSeconds().ToString()),
            new("userId", user.Id.ToString()),
            new("username", user.UserName),
        };

        claims.AddRange( claimList );
        claims.AddRange(roleList);
        claims = claims.Distinct().ToList();
        
        var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _jwtSettings.SecretKey ) );
        var credentials = new SigningCredentials( key, SecurityAlgorithms.HmacSha256 );
        var expiryDate = DateTime.UtcNow.AddMinutes( _jwtSettings.ExpiryInMinutes );
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiryDate,
            signingCredentials: credentials );

        var jwtToken = new JwtToken();
        jwtToken.AccessToken = new JwtSecurityTokenHandler().WriteToken( token );
        jwtToken.ExpiryInMinutes = _jwtSettings.ExpiryInMinutes;
        jwtToken.ExpiryDate = expiryDate;
        return jwtToken;
    }
}