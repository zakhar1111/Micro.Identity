using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Movies.Client.Pages;

public partial class MoviesIndex : ComponentBase
{
    private IEnumerable<Movie> movies;
    [Inject] private IHttpClientFactory httpClientFactory { get; set; }
    [Inject] private IConfiguration Config { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var client = httpClientFactory.CreateClient();
        //client.BaseAddress = new Uri(Config["apiUrl"]);
        //movies = (IEnumerable<Movie>) await client.GetFromJsonAsync<Movie>($"/api/Movies");

        var apiUrl = Config["apiUrl"];
        var result = await client.GetAsync($"{apiUrl}/api/Movies");
        result.EnsureSuccessStatusCode();

        movies = await result.Content.ReadFromJsonAsync<IEnumerable<Movie>>();

    }
}
