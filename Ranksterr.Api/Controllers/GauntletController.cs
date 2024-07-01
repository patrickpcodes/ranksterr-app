using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Ranksterr.Domain.Users;
using Ranksterr.Infrastructure;
using TMDbLib.Objects.Authentication;

namespace Ranksterr.Api.Controllers;

[ApiController]
[Route("api/")]
public class GauntletController : ControllerBase
{
    private IConfiguration _configuration;
    private UserManager<ApplicationUser> _userManager;

    public GauntletController( IConfiguration configuration, UserManager<ApplicationUser> userManager )
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpGet( "gauntlet" )]
    public async Task<IResult> RunFirstTest()
    {

        var user = await TokenController.GetUserFromClaimsPrincipal( HttpContext.User, _userManager );
        RunStuff(user.Id);

        return Results.Ok();
    }

    [HttpGet( "gauntlet/{id}" )]
    public async Task<IResult> GetGauntletId( Guid id )
    {
        var connectionString = _configuration.GetConnectionString( "Database" );
        var rosterService = new RosterService( new SqlConnection( connectionString ) );
        
        rosterService.GetGauntletStats(id);

        return Results.Ok();
    }

    public void RunStuff(Guid userId)
    {
        var connectionString = _configuration.GetConnectionString( "Database" );
        var rosterService = new RosterService( new SqlConnection( connectionString ) );

        // Create a new roster
        var rosterId = rosterService.CreateRoster( userId, "My Favorite Items",
            "A collection of my favorite movies, songs, and paintings." );

        // Add a movie to the database and roster
        var movieId = Guid.NewGuid();
        rosterService.CreateMovie( movieId, "Inception", "Christopher Nolan", 2010,
            "http://example.com/inception.jpg" );
        rosterService.CreateItemToRoster( rosterId, movieId );

        // Add two songs to the database and roster
        var songId1 = Guid.NewGuid();
        rosterService.CreateSong( songId1, "Bohemian Rhapsody", "Queen", "A Night at the Opera", 1975,
            "http://example.com/bohemian_rhapsody.jpg" );
        rosterService.CreateItemToRoster( rosterId, songId1 );

        var songId2 = Guid.NewGuid();
        rosterService.CreateSong( songId2, "Imagine", "John Lennon", "Imagine", 1971,
            "http://example.com/imagine.jpg" );
        rosterService.CreateItemToRoster( rosterId, songId2 );

        // Create a gauntlet
        var gauntletId = rosterService.CreateGauntlet();

        // Add items from the roster to the gauntlet
        rosterService.CreateItemToGauntlet( gauntletId, movieId );
        rosterService.CreateItemToGauntlet( gauntletId, songId1 );
        rosterService.CreateItemToGauntlet( gauntletId, songId2 );

        // Add duels to the gauntlet
        var duelId1 = rosterService.CreateDuel( gauntletId, movieId, songId1 );
        var duelId2 = rosterService.CreateDuel( gauntletId, movieId, songId2 );
        var duelId3 = rosterService.CreateDuel( gauntletId, songId1, songId2 );

        // Pick winners for each duel
        rosterService.PickDuelWinner( duelId1, movieId ); // Assume this is the winner for the first duel
        rosterService.PickDuelWinner( duelId2, songId2 ); // Assume this is the winner for the second duel
        rosterService.PickDuelWinner( duelId3, songId1 ); // Assume this is the winner for the third duel

        // Retrieve and display the gauntlet stats
        rosterService.GetGauntletStats( gauntletId );
    }
}