using Movies.Client.Models;

namespace Movies.Client.Services;

public interface IMovieApiService
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie> GetMovieAsync(int id);
    Task CreateMovieAsync(Movie movie);
    Task UpdateMovieAsync(int id, Movie movie);
    Task DeleteMovieAsync(int id);
}
