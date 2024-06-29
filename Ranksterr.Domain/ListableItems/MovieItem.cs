using Newtonsoft.Json.Linq;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.ListableItems;

namespace Ranksterr.Domain.Listables;

public class MovieItem : ListItem
{
    public string Thumbnail { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? TmdbId { get; set; }
    public string? ImdbId { get; set; }
    public override void PutJson(JObject json)
    {
        Id = JsonUtilities.GetGuid(json, "id");
        Title = JsonUtilities.GetString(json, "title");
        Thumbnail = JsonUtilities.GetString(json, "posterPath");
        ReleaseDate = JsonUtilities.GetNullableDateTime(json, "releaseDate");
        TmdbId = JsonUtilities.GetString( json, "tmdbId" );
        ImdbId = JsonUtilities.GetString( json, "imdbId" );
    }

    public override JObject GetJson()
    {
        var json = new JObject();

        JsonUtilities.Set(json, "id", Id);
        JsonUtilities.Set(json, "title", Title);
        JsonUtilities.Set(json, "posterPath", Thumbnail);
        JsonUtilities.Set(json, "releaseDate", ReleaseDate);
        JsonUtilities.Set(json, "tmdbId", TmdbId  );
        JsonUtilities.Set(json, "imdbId", ImdbId  );
        
        return json;
    }
}