using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using Movies.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Movies.Client.Pages;


public partial class MoviesIndex : ComponentBase
{
    private IEnumerable<Movie> movies;

    [Inject] private IMovieApiService MovieService { get; set; }

    protected override async Task OnInitializedAsync() =>
        movies = await MovieService.GetMoviesAsync();
}