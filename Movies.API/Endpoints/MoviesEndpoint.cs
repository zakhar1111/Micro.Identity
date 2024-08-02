using Carter;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Models;

namespace Movies.API.Endpoints;

public class MoviesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Movies", async (MoviesContext context) =>
        {
            var result = await context.GetMovies();
            return Results.Ok(result);
        }).RequireAuthorization("ApiScope");

        app.MapGet("/api/Movies/{id}", async (MoviesContext context, int id) =>
        {
            var res = await context.GetMovie(id);
            return Results.Ok(res);
        }).RequireAuthorization("ApiScope");

        app.MapPut("/api/Movies/{id}", async (MoviesContext context, int id, Movie movie) => 
        { 
            await context.PutMovie(movie, id);
            return Results.NoContent();
        }).RequireAuthorization("ApiScope");

        app.MapPost("/api/Movies", async (MoviesContext context, Movie movie) =>
        {
            await context.PostMovie(movie);
            return Results.Created($"{movie.Id}", movie);
        }).RequireAuthorization("ApiScope");

        app.MapDelete("/api/Movies/{id}", async (MoviesContext context, int id) => 
        { 
            var res = await context.DeleteMovie(id);
            return Results.Ok(res);
        }).RequireAuthorization("ApiScope");


    }
}
