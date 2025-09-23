using System.Net.Http.Json;
using RevrenLove.SimplishAuth.Models.Requests;
using RevrenLove.SimplishAuth.Models.Responses;

namespace RevrenLove.SimplishAuth;

internal class SimplishAuthClient(HttpClient httpClient) : ISimplishAuthClient
{
    private readonly HttpClient _httpClient = httpClient;

    // TODO: Document this method... like for the "Request" stripping
    private async Task<HttpResponseMessage> MakeRequest<T>(T request, Dictionary<string, string>? queryParameters = null)
        where T : notnull
    {
        var route =
            request
                .GetType()
                .Name
                .Replace("Request", string.Empty)
                .ShrinkFirstLetter();

        if (queryParameters is not null && queryParameters.Count > 0)
        {
            var queryString = string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            route = $"{route}?{queryParameters}";
        }

        var response = await _httpClient.PostAsJsonAsync(route, request);

        // if (!response.IsSuccessStatusCode)
        // {
        //     HandleUnsuccessfulStatusCode(response);
        // }

        return response;
    }

    // private async Task<TResponse> MakeRequest<TRequest, TResponse>(TRequest request, Dictionary<string, string>? queryParameters = null)
    //     where TRequest : notnull
    // {
    //     var responseMessage = await MakeRequest(request, queryParameters);

    //     if (responseMessage.IsSuccessStatusCode)
    //     {
    //         var response = (await responseMessage.Content.ReadFromJsonAsync<TResponse>())!;

    //         return response;
    //     }

    //     if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
    //     {
    //         // TODO: JE - This seems like magic... document it...
    //         var response = (await responseMessage.Content.ReadFromJsonAsync<HttpValidationProblemDetails>())!;
    //     }
    // }

    // private static void HandleUnsuccessfulStatusCode(HttpResponseMessage response)
    // {

    // }



    public Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<AccessTokenResponse> Login(LoginRequest loginRequest, bool? useCookies = null, bool? useSessionCookies = null)
    {
        // Dictionary<string, string> queryParameters = new()
        // {
        //     { nameof(useCookies), $"{useCookies ?? false}" },
        //     { nameof(useSessionCookies), $"{useSessionCookies ?? false}" },
        // };

        // var response = await MakeRequest(loginRequest, queryParameters);

        // return response;

        throw new NotImplementedException();
    }

    public Task<TwoFactorResponse> Manage2Fa(string bearerToken, TwoFactorRequest twoFactorRequest)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResponse> ManageInfo(string bearerToken)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResponse> ManageInfo(string bearerToken, InfoRequest infoRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AccessTokenResponse> Refresh(RefreshRequest refreshRequest)
    {
        throw new NotImplementedException();
    }

    public async Task Register(RegisterRequest registerRequest) =>
        await MakeRequest(registerRequest);

    public Task ResendConfirmationEmail(ResendConfirmationEmailRequest resendConfirmationEmailRequest)
    {
        throw new NotImplementedException();
    }

    public Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        throw new NotImplementedException();
    }
}