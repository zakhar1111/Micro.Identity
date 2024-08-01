﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Movies.Client.Pages;

public partial class MovieDetails : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    private Movie movie = new Movie();
    [Inject] private IHttpClientFactory httpClientFactory { get; set; }
    [Inject] private IConfiguration Config { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var client = httpClientFactory.CreateClient();
        var apiUrl = Config["apiUrl"];

        var result = await client.GetAsync($"{apiUrl}/api/Movies/{this.Id}");
        result.EnsureSuccessStatusCode();

        movie = await result.Content.ReadFromJsonAsync<Movie>();
    }

    private void NavigateToEdit()
    {
        Navigation.NavigateTo($"/movieedit/{movie.Id}");
    }
}
