using System.Net.Http.Json;

namespace RevrenLove.SimplishAuth.Client;

// TODO: JE - Document this _and_ the interface
// TODO: JE - Figure out how we want to do exceptions... _if_ we want to do them. Validation might already take care of that though...
internal class SimplishAuthClient(HttpClient httpClient) : ISimplishAuthClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<SimplishAuthClientResult> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Register).ToLower(), request);

        var result = await SimplishAuthClientResult.CreateAsync(response, isValidated: true);

        return result;
    }

    public async Task<SimplishAuthClientResult> Register(string email, string password) =>
        await
            Register(new() { Email = email, Password = password });

    public async Task<SimplishAuthClientResult<AccessTokenResponse>> Login(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Login).ToLower(), request);

        var result = await SimplishAuthClientResult<AccessTokenResponse>.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult<AccessTokenResponse>> Login(string email, string password) =>
        await
            Login(new() { Email = email, Password = password });

    public async Task<SimplishAuthClientResult> LoginWithCookies(LoginRequest request)
    {
        var route = $"{nameof(Login).ToLower()}?useCookies=true";

        var response = await _httpClient.PostAsJsonAsync(route, request);

        var result = await SimplishAuthClientResult.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> LoginWithCookies(string email, string password) =>
        await
            LoginWithCookies(new() { Email = email, Password = password });

    public async Task<SimplishAuthClientResult> LoginWithSessionCookies(LoginRequest request)
    {
        var route = $"{nameof(Login).ToLower()}?useSessionCookies=true";

        var response = await _httpClient.PostAsJsonAsync(route, request);

        var result = await SimplishAuthClientResult.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> LoginWithSessionCookies(string email, string password) =>
        await
            LoginWithSessionCookies(new() { Email = email, Password = password });

    public async Task<SimplishAuthClientResult<AccessTokenResponse>> Refresh(RefreshRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Refresh).ToLower(), request);

        var result = await SimplishAuthClientResult<AccessTokenResponse>.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> ResendConfirmationEmail(ResendConfirmationEmailRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ResendConfirmationEmail).ToLower(), request);

        var result = await SimplishAuthClientResult.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ForgotPassword).ToLower(), request);

        var result = await SimplishAuthClientResult.CreateAsync(response, isValidated: true);

        return result;
    }

    public async Task<SimplishAuthClientResult> ResetPassword(ResetPasswordRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ResetPassword).ToLower(), request);

        var result = await SimplishAuthClientResult.CreateAsync(response, isValidated: true);

        return result;
    }

    // manage/2fa
    public async Task<SimplishAuthClientResult<TwoFactorResponse>> Manage2Fa(TwoFactorRequest request)
    {
        var route = "manage/2fa";

        var response = await _httpClient.PutAsJsonAsync(route, request);

        var result = await SimplishAuthClientResult<TwoFactorResponse>.CreateAsync(response, isValidated: true);

        return result;
    }

    // manage/info
    public async Task<SimplishAuthClientResult<InfoResponse>> ManageInfo()
    {
        var route = "manage/info";

        var response = await _httpClient.GetAsync(route);

        var result = await SimplishAuthClientResult<InfoResponse>.CreateAsync(response, isValidated: true);

        return result;
    }


    // manage/info
    public async Task<SimplishAuthClientResult<InfoResponse>> ManageInfo(InfoRequest request)
    {
        var route = "manage/info";

        var response = await _httpClient.PostAsJsonAsync(route, request);

        var result = await SimplishAuthClientResult<InfoResponse>.CreateAsync(response, isValidated: true);

        return result;
    }

    public async Task<SimplishAuthClientResult> Logout()
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Logout).ToLower(), new { });

        var result = await SimplishAuthClientResult.CreateAsync(response);

        return result;
    }
}
