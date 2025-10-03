using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public class WeatherForecastClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    private static readonly string _resource = "WeatherForecast";

    public async Task<WeatherForecast[]> Get() =>
        await
            _httpClient
                .GetFromJsonAsync<WeatherForecast[]>(_resource) ?? [];

    public async Task<WeatherForecast> GetSecure(string bearerToken)
    {
        var responseMessage = await SecureRequestHelper(HttpMethod.Get, "secure", bearerToken);

        var responseBody = await responseMessage.Content.ReadFromJsonAsync<WeatherForecast>();

        return responseBody!;
    }

    private async Task<HttpResponseMessage> SecureRequestHelper(HttpMethod httpMethod, string route, string bearerToken, object? requestBody = null)
    {
        var requestMessage = new HttpRequestMessage(httpMethod, route);

        requestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");

        if (requestBody is not null)
        {
            var json = JsonSerializer.Serialize(requestBody);

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(requestMessage);

        return response;
    }
}
