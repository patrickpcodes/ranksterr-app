using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Ranksterr.Api.Extensions;
using Ranksterr.Application.Abstractions;
using Ranksterr.Application.Authentication;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Authentication;
using Ranksterr.Infrastructure;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using static Ranksterr.Infrastructure.AuthorizationConstants;

namespace Ranksterr.Api;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );
        //Need to set this up if dont want all schemas text crap in JWT claims
        //https://github.com/dotnet/aspnetcore/issues/4660
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
       
        // Add services to the container.
        builder.Services.AddCors();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        
        builder.Services.Configure<JwtSettings>( builder.Configuration.GetSection( "JwtSettings" ) );
        builder.Services.AddSingleton<IJwtService, JwtService>();
        builder.Services.AddSingleton<ISettingsService<JwtSettings>, SettingsService<JwtSettings>>();
        builder.Services.AddCors( options => options.AddPolicy( "AllowAll", p => p.AllowAnyOrigin()
                                                                                  .AllowAnyMethod()
                                                                                  .AllowAnyHeader() ) );
        //TODO make sure order is correct on this
        builder.Services.AddInfrastructure( builder.Configuration );
        builder.Services.AddAuthentication( options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               } )
               .AddJwtBearer( options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                       ValidAudience = builder.Configuration["JwtSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration["JwtSettings:SecretKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });
        builder.Services.AddAuthorization(options =>{
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            
            //TODO can require Role directly, don't need to use claims to handle rolls
            //Need to test this out
            //Can have separate policies for claims, like if people had separate data in their claims
            //Like employee ID, or TITLE, or something else that we save
            //Also can have just a claim or role, not necessarily a specific claim or role
            options.AddPolicy( ClaimPolicyIsAdmin, policy =>
                policy.RequireClaim( ClaimAccessLevelType, ClaimAccessLevelAdmin ) );
        } ); 
        
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            
            app.ApplyMigrations();
            UserSeeding.SeedApplication(app);
        }

        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors( b =>
            b.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader() );

        app.MapControllers();
        app.MapFallbackToFile( "index.html" );
        
        // app.MapEndpoints();
        //
        // app.MapGet( "/api/values", async ( ILogger<Program> logger, IConfiguration configuration ) =>
        // {
        //     TMDbClient client = new TMDbClient( configuration["TMDB-API-KEY"] );
        //     client.GetMovieAsync( 123, TMDbLib.Objects.Movies.MovieMethods.Credits );
        //
        //
        //     var cols = new List<string>() { "James Bond" }; //, "Die Hard", "Matrix", "Batman", "Harry Potter" };
        //     Random rnd = new Random();
        //     SearchContainer<SearchCollection> collectons =
        //         await client.SearchCollectionAsync( cols[rnd.Next( cols.Count )] );
        //     Console.WriteLine( $"Got {collectons.Results.Count:N0} collections" );
        //
        //     var jamesBonds = client.GetCollectionAsync( collectons.Results.First().Id ).Result;
        //     Console.WriteLine( $"Collection: {jamesBonds.Name}" );
        //     Console.WriteLine();
        //
        //     Console.WriteLine( $"Got {jamesBonds.Parts.Count:N0} James Bond Movies" );
        //     foreach ( SearchMovie part in jamesBonds.Parts.OrderBy( c => c.ReleaseDate ) )
        //         Console.WriteLine( $"{part.Title} ({part.ReleaseDate.Value.Year})" );
        //
        //     var items = new JArray();
        //
        //     foreach ( SearchMovie part in jamesBonds.Parts.OrderBy( c => c.ReleaseDate ) )
        //     {
        //         var zax = new TmdbMovieItem()
        //         {
        //             Id = part.Id,
        //             Title = part.Title,
        //             Thumbnail = $@"https://image.tmdb.org/t/p/original{part.PosterPath}",
        //             ReleaseDate = part.ReleaseDate
        //         };
        //         items.Add( zax.GetJson() );
        //     }
        //
        //     // Convert JArray to JSON string
        //     string json = items.ToString();
        //
        //     return Results.Content( json, "application/json" );
        // } );
        //
        // app.Urls.Add( "https://0.0.0.0:7297" );

        app.Run();
    }
}