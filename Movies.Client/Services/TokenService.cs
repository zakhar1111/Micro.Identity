using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Movies.Client.Services;

public class TokenService : ITokenService
{
    public readonly IOptions<IdentityServerSettings> identityServerSettings;
    public readonly DiscoveryDocumentResponse discoveryDocument;
    private readonly IHttpClientFactory httpClient;

    public TokenService(IOptions<IdentityServerSettings> identityServerSettings, 
        IHttpClientFactory httpClient)
    {
        this.identityServerSettings = identityServerSettings;
        this.httpClient = httpClient;

        var client = httpClient.CreateClient();
        discoveryDocument = client.GetDiscoveryDocumentAsync(this.identityServerSettings.Value.DiscoveryUrl).Result;

        if (discoveryDocument.IsError)
        {
            throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
        }
    }

    public async Task<TokenResponse> GetToken(string scope)
    {
        var client = httpClient.CreateClient();
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = identityServerSettings.Value.ClientName,
            ClientSecret = identityServerSettings.Value.ClientPassword,
            Scope = scope
        });

        if (tokenResponse.IsError)
        {
            throw new Exception("Unable to get token", tokenResponse.Exception);
        }

        return tokenResponse;
    }
}

