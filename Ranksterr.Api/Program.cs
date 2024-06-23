using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Ranksterr.Domain.Tmdb;
using Ranksterr.Infrastructure;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Ranksterr.Api;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
        builder.Services.AddCors();
        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors( options => options.AddPolicy( "AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader() ) );
//TODO make sure order is correct on this
        builder.Services.AddInfrastructure( builder.Configuration );

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthorization();

        app.UseCors( b =>
            b.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader() );

        app.MapControllers();
        app.MapFallbackToFile( "index.html" );

// app.MapEndpoints();

// app.MapGet("/api/values", async (ILogger<Program> logger, IConfiguration configuration) =>
// {
//   TMDbClient client = new TMDbClient(configuration["TMDB-API-KEY"]);
//   client.GetMovieAsync(123, TMDbLib.Objects.Movies.MovieMethods.Credits);
//
//
//   var cols = new List<string>() { "James Bond"} ; //, "Die Hard", "Matrix", "Batman", "Harry Potter" };
//   Random rnd = new Random();
//   SearchContainer<SearchCollection> collectons = await client.SearchCollectionAsync(cols[rnd.Next(cols.Count)]);
//   Console.WriteLine($"Got {collectons.Results.Count:N0} collections");
//
//   var jamesBonds = client.GetCollectionAsync(collectons.Results.First().Id).Result;
//   Console.WriteLine($"Collection: {jamesBonds.Name}");
//   Console.WriteLine();
//
//   Console.WriteLine($"Got {jamesBonds.Parts.Count:N0} James Bond Movies");
//   foreach (SearchMovie part in jamesBonds.Parts.OrderBy(c => c.ReleaseDate))
//     Console.WriteLine($"{part.Title} ({part.ReleaseDate.Value.Year})");
//
//   var items = new JArray();
//
//   foreach (SearchMovie part in jamesBonds.Parts.OrderBy(c => c.ReleaseDate))
//   {
//     var zax = new TmdbMovieItem()
//     {
//       Id = part.Id,
//       Title = part.Title,
//       Thumbnail = $@"https://image.tmdb.org/t/p/original{part.PosterPath}",
//       ReleaseDate = part.ReleaseDate
//     };
//     items.Add(zax.GetJson());
//   }
//
//   // Convert JArray to JSON string
//   string json = items.ToString();
//
//   return Results.Content(json, "application/json");
// });

//app.Urls.Add("https://0.0.0.0:7297");

        app.Run();
    }
}