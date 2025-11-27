using System.Net.Http.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IWeatherForecastClient
{
    Task<WeatherForecast[]> Get();
    Task<WeatherForecast[]> GetSecure(string token);
}

internal class WeatherForecastClient(HttpClient httpClient) : IWeatherForecastClient
{
    private static readonly string _resource = "WeatherForecast";

    public async Task<WeatherForecast[]> Get() =>
        await
            httpClient
                .GetFromJsonAsync<WeatherForecast[]>(_resource) ?? [];

    public async Task<WeatherForecast[]> GetSecure(string token) =>
        await
            httpClient
                .GetFromJsonAsync<WeatherForecast[]>(token, _resource) ?? [];
}
