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


namespace Movies.Client.Pages;

public partial class MovieEdit : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    private Movie movie = new Movie();
    [Inject] private IHttpClientFactory httpClientFactory { get; set; }
    [Inject] private IConfiguration Config { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var client = httpClientFactory.CreateClient();
        var apiUrl = Config["apiUrl"];

        var result = await client.GetAsync($"{apiUrl}/api/Movies/{Id}");
        result.EnsureSuccessStatusCode();

    }

    private async Task HandleValidSubmit()
    {
        var client = httpClientFactory.CreateClient();
        var apiUrl = Config["apiUrl"];

        movie.Id = this.Id;
        movie.Rating = "0.0";
        movie.ImageUrl = "images/src";

        var jsonMovie = new StringContent(JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");

        var result = await client.PutAsync($"{apiUrl}/api/Movies/{this.Id}", jsonMovie);
        result.EnsureSuccessStatusCode();

    }
}
