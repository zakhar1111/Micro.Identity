using Microsoft.EntityFrameworkCore;
using Movies.API.Exceptions;
using Movies.API.Models;

namespace Movies.API.Data;

public static class MoviesContextExtension
{
    public static async Task<IEnumerable<Movie>> GetMovies(this MoviesContext context) =>
        await context.Movie.ToListAsync();
    public static async Task<Movie> GetMovie(this MoviesContext context, int id)
    {
        var result = await context.Movie.FindAsync(id);
        if (result == null)
            throw new NotFoundMovie(typeof(Movie));
        return result;
    }
    public static async Task PutMovie(this MoviesContext context, Movie movie, int id)
    {
        if (id != movie.Id)
        {
            throw new  BadIdMovie(typeof(Movie));
        }

        context.Entry(movie).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Movie.Any(e => e.Id == id))
                throw new NotFoundMovie(typeof(Movie));
            else
                throw;
        }
    }

    public static async Task PostMovie(this MoviesContext context, Movie movie)
    { 
        await context.Movie.AddAsync(movie);
        await context.SaveChangesAsync();
    }

    public static async Task<Movie> DeleteMovie(this MoviesContext context, int id)
    {
        var movie = await context.Movie.FindAsync(id);
        if (movie == null)
            throw new NotFoundMovie(typeof(Movie));

        context.Movie.Remove(movie);
        await context.SaveChangesAsync();
        return movie;
    }
 
}