using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ranksterr.Domain.ListableItems;
using Ranksterr.Domain.Listables;
using Ranksterr.Infrastructure;

namespace Ranksterr.Api.Controllers;
[ApiController]
[Route("api/")]
public class TestingController : ControllerBase
{
    private ApplicationDbContext _context;

    public TestingController( ApplicationDbContext context )
    {
        _context = context;
    }

    [HttpGet( "create" )]
    public async Task<IResult> CreateMovies()
    {
        var movie = new MovieItem { Title = "Silence of the Lambs", TmdbId = "12345"};
        var tvShowItem = new TvShowItem{ Title = "Seinfeld", ImdbId = "98761"};
        var tvShowEpisode = new TvShowEpisodeItem { Title = "Simpsons Pilot", Thumbnail = "url link" };

        var newList = new ItemList
        {
            Name = "My Item List",
            Description = "A new ItemList",
            ListItemAssignments = new List<ListItemAssignment>
            {
                new ListItemAssignment { ListItem = movie },
                new ListItemAssignment { ListItem = tvShowItem },
                new ListItemAssignment { ListItem = tvShowEpisode },
            }
        };

           _context.ItemLists.Add(newList);
           await _context.SaveChangesAsync();
           return Results.Ok(new {newList.Id});
    }

    [HttpGet( "retrieve" )]
    public async Task<IResult> GetMovies()
    {
        
        var lists = _context.ItemLists
                           .Select(l => new
                           {
                               List = l,
                               ListItemAssignments = l.ListItemAssignments.Select(lia => new
                               {
                                   lia.ListItemId,
                                   lia.ListItem.Title,
                                   Type = _context.Entry(lia.ListItem).Property<string>("Discriminator").CurrentValue 
                               })
                           })
                           .ToList();

        foreach (var list in lists)
        {
            Console.WriteLine($"List Name: {list.List.Name}");
            Console.WriteLine($"Description: {list.List.Description}");
            foreach (var assignment in list.ListItemAssignments)
            {
                Console.WriteLine($"- {assignment.Title} ({assignment.Type})");
            }
            Console.WriteLine();
        }

        return Results.Ok( new { lists } );
    }
    
    
    
}