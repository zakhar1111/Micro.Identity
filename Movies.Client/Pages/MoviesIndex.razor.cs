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
    [Inject] private IHttpClientFactory httpClientFactory { get; set; }
    [Inject] private IConfiguration Config { get; set; }
    [Inject] private ITokenService TokenService { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var client = httpClientFactory.CreateClient();

        //bearer token
        var tokenResponse = await TokenService.GetToken("MoviesAPI.read");
        client.SetBearerToken(tokenResponse.AccessToken);
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

        var apiUrl = Config["apiUrl"];
        var result = await client.GetAsync($"{apiUrl}/api/Movies");
        result.EnsureSuccessStatusCode();

        movies = await result.Content.ReadFromJsonAsync<IEnumerable<Movie>>();

    }
}
