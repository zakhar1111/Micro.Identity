using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using Movies.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Movies.Client.Pages;

public partial class MovieDetails : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    private Movie movie = new Movie();

    [Inject] private IMovieApiService MovieService { get; set; }

    [Inject] private NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync() =>
        movie = await MovieService.GetMovieAsync(Id);

    private void NavigateToEdit() =>
        Navigation.NavigateTo($"/movieedit/{movie.Id}");
}

