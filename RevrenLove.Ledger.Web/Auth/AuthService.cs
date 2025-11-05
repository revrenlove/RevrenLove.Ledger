using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using RevrenLove.Ledger.Api.Client;
using RevrenLove.SimplishAuth.Models.Requests;

namespace RevrenLove.Ledger.Web.Auth;

public class AuthService(
    HttpClient http,
    ILedgerApiClient ledgerApiClient,
    AuthenticationStateProvider provider)
{
    private readonly HttpClient _http = http;
    private readonly ILedgerApiClient _ledgerApiClient = ledgerApiClient;
    private readonly CookieAuthenticationStateProvider _provider = (CookieAuthenticationStateProvider)provider;

    public async Task<bool> LoginAsync(string email, string password)
    {
        var loginResponse = await _ledgerApiClient.SimplishAuth.Login(new() { Email = email, Password = password });
        if (!loginResponse.IsSuccessStatusCode)
        {
            return false;
        }

        // TODO: JE - Make this not a magic string...
        var me = await _http.GetFromJsonAsync<UserInfo>("user");
        if (me != null) _provider.NotifyAuthenticated(me);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _http.PostAsync("/logout", null);
        _provider.NotifyLoggedOut();
    }
}
