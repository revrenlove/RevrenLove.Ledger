using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using RevrenLove.Ledger.Api.Client;
using RevrenLove.Ledger.Web.Auth.Models;

namespace RevrenLove.Ledger.Web.Auth;

// TODO: JE - Give this whole class a good once over
public class CookieAuthenticationStateProvider(
    ILedgerApiClient ledgerApiClient,
    ILogger<CookieAuthenticationStateProvider> logger)
    : AuthenticationStateProvider, IAccountManagement
{
    private bool _isAuthenticated = false;

    private readonly ClaimsPrincipal _unauthenticatedUser = new(new ClaimsIdentity());

    public async Task<FormResult> RegisterAsync(string email, string password)
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            var result = await ledgerApiClient.SimplishAuthClient.Register(email, password);

            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            var errors =
                result
                    .HttpValidationProblemDetails?
                    .Errors
                    .Select(e => $"{e.Key}: {string.Join(", ", e.Value)}");

            return new FormResult
            {
                Succeeded = false,
                ErrorList = errors != null && errors.Any() ? [.. errors] : defaultDetail
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "App error");
        }

        // unknown error
        return new FormResult
        {
            Succeeded = false,
            ErrorList = defaultDetail
        };
    }

    public async Task<FormResult> LoginAsync(string email, string password)
    {
        try
        {
            var result = await ledgerApiClient.SimplishAuthClient.LoginWithCookies(email, password);

            if (result.IsSuccessStatusCode)
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                return new FormResult { Succeeded = true };
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "App error");
        }

        return new FormResult
        {
            Succeeded = false,
            ErrorList = ["Invalid email and/or password."]
        };
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;

        var user = _unauthenticatedUser;

        try
        {
            var userInfo = await ledgerApiClient.SimplishAuthClient.ManageInfo();

            if (userInfo.Value != null)
            {
                var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userInfo.Value.Email),
                        new(ClaimTypes.Email, userInfo.Value.Email),
                    };

                var claimsIdentity = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));

                user = new(claimsIdentity);

                _isAuthenticated = true;
            }
        }
        catch (Exception ex) when (ex is HttpRequestException exception)
        {
            if (exception.StatusCode != HttpStatusCode.Unauthorized)
            {
                logger.LogError(ex, "App error");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "App error");
        }

        return new AuthenticationState(user);
    }

    public async Task LogoutAsync()
    {
        await ledgerApiClient.SimplishAuthClient.Logout();

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();

        return _isAuthenticated;
    }

    ///// <summary>
    ///// Authentication state.
    ///// </summary>
    //private bool _isAuthenticated = false;

    //// TODO: JE - Is there a better way to do this?
    //private readonly ClaimsPrincipal _unauthenticatedUser = new(new ClaimsIdentity());

    //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //    var result = await ledgerApiClient.SimplishAuthClient.ManageInfo();

    //    if (!result.IsSuccessStatusCode || result.Value is null)
    //    {
    //        // TODO: JE - Differentiate between 4xx and 5xx...
    //        return new(_unauthenticatedUser);
    //    }

    //    var claims = new List<Claim>
    //    {
    //        new(ClaimTypes.Name, result.Value.Email),
    //        new(ClaimTypes.Email, result.Value.Email),
    //    };

    //    var identity = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
    //    var user = new ClaimsPrincipal(identity);

    //    _isAuthenticated = true;

    //    return new(user);
    //}

    //public async Task<FormResult> LoginAsync(string email, string password)
    //{
    //    var result = await ledgerApiClient.SimplishAuthClient.LoginWithCookies(email, password);

    //    if (result.IsSuccessStatusCode)
    //    {
    //        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    //        return new FormResult { Succeeded = true };
    //    }

    //    return new()
    //    {
    //        // TODO: JE - Use the HttpValidationDetails object here...
    //        Succeeded = false,
    //        ErrorList = ["Invalid email and/or password."]
    //    };
    //}

    //public async Task LogoutAsync()
    //{
    //    await ledgerApiClient.SimplishAuthClient.Logout();

    //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    //}

    //public async Task<FormResult> RegisterAsync(string email, string password)
    //{
    //    var x = await ledgerApiClient.SimplishAuthClient.Register(email, password);

    //    if (x.IsSuccessStatusCode)
    //    {
    //        return new FormResult { Succeeded = true };
    //    }

    //    // TODO: JE - Parse HttpValidationErrors...
    //    return new FormResult { Succeeded = false };
    //}

    //public async Task<bool> CheckAuthenticatedAsync()
    //{
    //    await GetAuthenticationStateAsync();

    //    return _isAuthenticated;
    //}
}
