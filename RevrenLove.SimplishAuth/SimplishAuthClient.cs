using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using RevrenLove.SimplishAuth.Models.Requests;
using RevrenLove.SimplishAuth.Models.Responses;

namespace RevrenLove.SimplishAuth;

internal class SimplishAuthClient(
    HttpClient httpClient,
    ISimplishAuthClientResultFactory simplishAuthClientResultFactory)
    : ISimplishAuthClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<SimplishAuthClientResult> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Register).ToLower(), request);

        var result = await simplishAuthClientResultFactory.CreateAsync(response, isValidated: true);

        return result;
    }

    public async Task<SimplishAuthClientResult<AccessTokenResponse>> Login(LoginRequest request, bool useCookies = false, bool useSessionCookies = false)
    {
        var route = $"{nameof(Login).ToLower()}?{nameof(useCookies)}={useCookies}&{nameof(useSessionCookies)}={useSessionCookies}";

        var response = await _httpClient.PostAsJsonAsync(route, request);

        var result = await simplishAuthClientResultFactory.CreateAsync<AccessTokenResponse>(response);

        return result;
    }

    public async Task<SimplishAuthClientResult<AccessTokenResponse>> Refresh(RefreshRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(Refresh).ToLower(), request);

        var result = await simplishAuthClientResultFactory.CreateAsync<AccessTokenResponse>(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> ResendConfirmationEmail(ResendConfirmationEmailRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ResendConfirmationEmail).ToLower(), request);

        var result = await simplishAuthClientResultFactory.CreateAsync(response);

        return result;
    }

    public async Task<SimplishAuthClientResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ForgotPassword).ToLower(), request);

        var result = await simplishAuthClientResultFactory.CreateAsync(response, isValidated: true);

        return result;
    }

    public async Task<SimplishAuthClientResult> ResetPassword(ResetPasswordRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(nameof(ResetPassword).ToLower(), request);

        var result = await simplishAuthClientResultFactory.CreateAsync(response, isValidated: true);

        return result;
    }

    // manage/2fa
    public async Task<SimplishAuthClientResult<TwoFactorResponse>> Manage2Fa(string bearerToken, TwoFactorRequest request)
    {
        var route = "manage/2fa";

        var method = HttpMethod.Post;

        var requestMessage = new HttpRequestMessage(method, route);

        requestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var json = JsonSerializer.Serialize(request);
        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var result = await simplishAuthClientResultFactory.CreateAsync<TwoFactorResponse>(response, isValidated: true);

        return result;
    }

    // manage/info
    public async Task<SimplishAuthClientResult<InfoResponse>> ManageInfo(string bearerToken)
    {
        var route = "manage/info";

        var method = HttpMethod.Get;

        var requestMessage = new HttpRequestMessage(method, route);

        requestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var response = await _httpClient.SendAsync(requestMessage);

        var result = await simplishAuthClientResultFactory.CreateAsync<InfoResponse>(response, isValidated: true);

        return result;
    }


    // manage/info
    public async Task<SimplishAuthClientResult<InfoResponse>> ManageInfo(string bearerToken, InfoRequest request)
    {
        var route = "manage/info";

        var method = HttpMethod.Post;

        var requestMessage = new HttpRequestMessage(method, route);

        requestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");

        var json = JsonSerializer.Serialize(request);
        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var result = await simplishAuthClientResultFactory.CreateAsync<InfoResponse>(response, isValidated: true);

        return result;
    }
}
