namespace Ranksterr.Domain.Movie;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    // Additional properties
    public ICollection<MovieListMovie> MovieListMovies { get; set; }
}

public class MovieList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<MovieListMovie> MovieListMovies { get; set; }
    public ICollection<Battle> Battles { get; set; }
}

public class MovieListMovie
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int MovieListId { get; set; }
    public MovieList MovieList { get; set; }
}

public class Battle
{
    public int Id { get; set; }
    public int MovieListId { get; set; }
    public MovieList MovieList { get; set; }
    public int Movie1Id { get; set; }
    public Movie Movie1 { get; set; }
    public int Movie2Id { get; set; }
    public Movie Movie2 { get; set; }
    public BattleResult BattleResult { get; set; }
}

public class BattleResult
{
    public int Id { get; set; }
    public int BattleId { get; set; }
    public Battle Battle { get; set; }
    public int WinnerMovieId { get; set; }
    public Movie WinnerMovie { get; set; }
}

