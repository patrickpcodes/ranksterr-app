using Newtonsoft.Json.Linq;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Listables;

namespace Ranksterr.Domain.Tmdb;

public class TmdbMovieItem : Listable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public override void PutJson(JObject json)
    {
        Id = JsonUtilities.GetInt(json, "id");
        Title = JsonUtilities.GetString(json, "title");
        Thumbnail = JsonUtilities.GetString(json, "posterPath");
        ReleaseDate = JsonUtilities.GetNullableDateTime(json, "releaseDate");
    }

    public override JObject GetJson()
    {
        var json = new JObject();

        JsonUtilities.Set(json, "id", Id);
        JsonUtilities.Set(json, "title", Title);
        JsonUtilities.Set(json, "posterPath", Thumbnail);
        JsonUtilities.Set(json, "releaseDate", ReleaseDate);

        return json;
    }
}