using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Movies.Client.Services;
using System.Net.Http.Headers;


namespace Movies.Client.Pages;

public partial class MovieDelete : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    private Movie movie = new Movie();

    [Inject] private IMovieApiService MovieService { get; set; }

    protected override async Task OnInitializedAsync() => 
        movie = await MovieService.GetMovieAsync(Id);

    private async Task DeleteMovie() =>
        await MovieService.DeleteMovieAsync(Id);
}