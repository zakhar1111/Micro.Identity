using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Movies.Client.Models;

namespace Movies.Client.Services;

public class MovieService : IMovieApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;

    public MovieService(IHttpClientFactory httpClientFactory, IConfiguration config, ITokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
        _tokenService = tokenService;
    }

    private async Task<HttpClient> CreateClientWithTokenAsync(string scope)
    {
        var client = _httpClientFactory.CreateClient();
        var tokenResponse = await _tokenService.GetToken(scope);
        if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
        {
            throw new Exception("Failed to retrieve token.");
        }
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
        return client;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        var client = await CreateClientWithTokenAsync("MoviesAPI.read");
        var apiUrl = _config["apiUrl"];
        var response = await client.GetAsync($"{apiUrl}/api/Movies");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Movie>>();
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        var client = await CreateClientWithTokenAsync("MoviesAPI.read");
        var apiUrl = _config["apiUrl"];
        var response = await client.GetAsync($"{apiUrl}/api/Movies/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Movie>();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var client = await CreateClientWithTokenAsync("MoviesAPI.read");
        var apiUrl = _config["apiUrl"];
        var response = await client.DeleteAsync($"{apiUrl}/api/Movies/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateMovieAsync(Movie movie)
    {
        var client = await CreateClientWithTokenAsync("MoviesAPI.read");
        var apiUrl = _config["apiUrl"];
        var jsonMovie = new StringContent(JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{apiUrl}/api/Movies", jsonMovie);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateMovieAsync(int id, Movie movie)
    {
        var client = await CreateClientWithTokenAsync("MoviesAPI.read");
        var apiUrl = _config["apiUrl"];

        var jsonMovie = new StringContent(JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{apiUrl}/api/Movies/{id}", jsonMovie);
        response.EnsureSuccessStatusCode();
    }

}

