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


namespace Movies.Client.Pages;

public partial class CreateNew : ComponentBase
{
    private Movie movie = new Movie();
    [Inject] private IHttpClientFactory httpClientFactory { get; set; }
    [Inject] private IConfiguration Config { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }


    protected  async Task HandleValidSubmit()
    {
        var client = httpClientFactory.CreateClient();
 
        var apiUrl = Config["apiUrl"];
        movie.Rating = "0.0";
        movie.ImageUrl = "images/src";
        var jsonMovie = new StringContent(JsonSerializer.Serialize(movie),Encoding.UTF8,Application.Json);

        var result = await client.PostAsync($"{apiUrl}/api/Movies", jsonMovie);
        result.EnsureSuccessStatusCode();

    }
}
