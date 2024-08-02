using IdentityModel.Client;

namespace Movies.Client.Services;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}
