using IdentityServer4.Models;

namespace IdentityServer;

public class Config
{
    public static string MovieAPI = "MovieAPI";
    public static string apiRead = "MovieAPI.read";
    public static string apiWrite = "MovieAPI.write";
    public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };
    public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope(apiRead), new ApiScope(apiWrite), };
    public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource(MovieAPI)
                {
                    Scopes = new List<string> { apiRead, apiWrite },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

    public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { apiRead, apiWrite }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", apiRead },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
            };
}
