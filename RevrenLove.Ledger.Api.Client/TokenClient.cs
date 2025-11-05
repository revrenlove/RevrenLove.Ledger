using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using RevrenLove.Ledger.Api.Client.Exceptions;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

internal interface ITokenClient
{

}

internal class TokenClient(LedgerApiHttpClient ledgerApiHttpClient) : ITokenClient
{
    private readonly HttpClient _httpClient = ledgerApiHttpClient.Instance;

    private static readonly string _resource = "Token";

    public async Task<JwtTokenResponse> RetrieveToken(EmailPasswordDto emailPasswordDto)
    {
        var x = await _httpClient.PostAsJsonAsync(_resource, emailPasswordDto);

        // TODO: JE - We need a scalable and abstracted way to parse the body of the response.
    }

    private static async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(
        HttpClient httpClient,
        string? requestUri,
        TRequest requestBody,
        JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var responseMessage = await httpClient.PostAsJsonAsync(requestUri, requestBody, options, cancellationToken: cancellationToken);

        if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedException(responseMessage);
        }

        TResponse responseBody;

        try
        {
            responseBody = (await responseMessage.Content.ReadFromJsonAsync<TResponse>(options, cancellationToken))!;
        }
        catch (Exception ex)
        {

        }


        // var responseBody = await responseMessage.Content.ReadFromJsonAsync<TResponse>(options, cancellationToken);

        return responseBody;
    }
}