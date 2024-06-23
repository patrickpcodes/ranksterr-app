using Microsoft.AspNetCore.Mvc;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Settings;

namespace Ranksterr.Api.Controllers;

[ApiController]
[Route("api/battle")]
public class BattleController : ControllerBase
{
    private readonly TmdbSettings _tmdbSettings;

    public BattleController(ISettingsService<TmdbSettings> tmdbSettingsService)
    {
        _tmdbSettings = tmdbSettingsService.GetSettings();
    }

    [HttpGet("")]
    public IResult GetBy()
    {
        var obs = new[] { "Test", "Test2" };
        return Results.Json(new {obs});
    }
}