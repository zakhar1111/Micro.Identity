using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Client.Pages;

public class LoginModel : PageModel
{
    public async Task<IActionResult> OnGetAsync(string redirectUri)
    {
        if (string.IsNullOrWhiteSpace(redirectUri))
        {
            redirectUri = Url.Content("~/");
        }

        if (HttpContext.User.Identity.IsAuthenticated)
        {
            Response.Redirect(redirectUri);
        }

        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = redirectUri
            },
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}
