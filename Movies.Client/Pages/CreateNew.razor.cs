using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Movies.Client.Services;
using System.Net.Http.Headers;


namespace Movies.Client.Pages;

public partial class CreateNew : ComponentBase
{
    private Movie movie = new Movie();
    [Inject] private IMovieApiService MovieService { get; set; }

    private async Task HandleValidSubmit()
    {
        movie.Rating = "0.0";
        movie.ImageUrl = "images/src";
        await MovieService.CreateMovieAsync(movie);
    }
}
