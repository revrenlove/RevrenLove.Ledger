using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;

namespace RevrenLove.Ledger.Web.Auth;

public class CookieAuthenticationStateProvider(HttpClient http) : AuthenticationStateProvider
{
    private readonly HttpClient _http = http;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // TODO: JE - Make this not a magic string...
            var me = await _http.GetFromJsonAsync<UserInfo>("user");
            if (me == null)
                return new AuthenticationState(_anonymous);

            var identity = new ClaimsIdentity(me.Claims.Select(c => new Claim(c.Type, c.Value)), "cookie");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyAuthenticated(UserInfo me)
    {
        var identity = new ClaimsIdentity(me.Claims.Select(c => new Claim(c.Type, c.Value)), "cookie");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public void NotifyLoggedOut()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}

public class UserInfo
{
    public string? Name { get; set; }
    public List<ClaimDto> Claims { get; set; } = [];
}

public class ClaimDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
