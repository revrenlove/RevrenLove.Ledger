using System.Net.Http.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IWeatherForecastClient
{
    Task<WeatherForecast[]> Get(CancellationToken cancellationToken = default);
    Task<WeatherForecast[]> GetSecure(CancellationToken cancellationToken = default);
}

internal class WeatherForecastClient(HttpClient httpClient) : IWeatherForecastClient
{
    public async Task<WeatherForecast[]> Get(CancellationToken cancellationToken = default) =>
        await
            httpClient
                .GetFromJsonAsync<WeatherForecast[]>(cancellationToken) ?? [];

    public async Task<WeatherForecast[]> GetSecure(CancellationToken cancellationToken = default) =>
        await
            httpClient
                .GetFromJsonAsync<WeatherForecast[]>("secure", cancellationToken) ?? [];
}
