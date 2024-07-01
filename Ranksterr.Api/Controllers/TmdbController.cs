using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Ranksterr.Domain.Movie;
using Ranksterr.Infrastructure;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Ranksterr.Api.Controllers;

[ApiController]
[Route("api/tmdb")]
public class TmdbController : ControllerBase
{
    private IConfiguration _configuration;
    // private ApplicationDbContext _context;

    public TmdbController( IConfiguration configuration  )
    {
        _configuration = configuration;
        // _context = context;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IResult> DownloadList( string id )
    {
        TMDbClient client = new TMDbClient( _configuration["TMDB-API-KEY"] );
        client.GetMovieAsync( 123, TMDbLib.Objects.Movies.MovieMethods.Credits );
        
        
        var cols = new List<string>() { "James Bond" }; //, "Die Hard", "Matrix", "Batman", "Harry Potter" };
        Random rnd = new Random();
        SearchContainer<SearchCollection> collectons =
            await client.SearchCollectionAsync( cols[rnd.Next( cols.Count )] );
        Console.WriteLine( $"Got {collectons.Results.Count:N0} collections" );
        
        var jamesBonds = client.GetCollectionAsync( collectons.Results.First().Id ).Result;
        Console.WriteLine( $"Collection: {jamesBonds.Name}" );
        Console.WriteLine();
        
        Console.WriteLine( $"Got {jamesBonds.Parts.Count:N0} James Bond Movies" );
        foreach ( SearchMovie part in jamesBonds.Parts.OrderBy( c => c.ReleaseDate ) )
            Console.WriteLine( $"{part.Title} ({part.ReleaseDate.Value.Year})" );
        
        var items = new List<Movie>();
        
        foreach ( SearchMovie part in jamesBonds.Parts.OrderBy( c => c.ReleaseDate ) )
        {
            var zax = new Movie()
            {
                Id = part.Id,
                Title = part.Title,
                ImageUrl = $@"https://image.tmdb.org/t/p/original{part.PosterPath}",
                ReleaseDate = part.ReleaseDate
            };
            items.Add( zax );
        }
        // _context.Movies.AddRange(items);
        //await _context.SaveChangesAsync();

        var itemList = new MovieList() { Id = 12, Name = "James Bond" };
        foreach ( var movieItem in items )
        {
            itemList.MovieListMovies.Add(new MovieListMovie(){Movie = movieItem, MovieList = itemList});
        }
        // _context.MovieLists.Add(it)
        
        // Convert JArray to JSON string
        string json = items.ToString();

        return Results.Ok();
    }
}