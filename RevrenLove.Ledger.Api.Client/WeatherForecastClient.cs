using System.Net.Http.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public class WeatherForecastClient(HttpClient httpClient)
{
    private static readonly string _resource = "WeatherForecast";

    public async Task<WeatherForecast[]> Get() =>
        await
            httpClient
                .GetFromJsonAsync<WeatherForecast[]>(_resource) ?? [];
}
