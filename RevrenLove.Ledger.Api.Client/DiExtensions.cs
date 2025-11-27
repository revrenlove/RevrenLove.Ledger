using RevrenLove.Ledger.Api.Client;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    private static readonly string _httpClientKey = Guid.NewGuid().ToString();

    public static IServiceCollection AddLedgerApiClientAsService(this IServiceCollection services, string baseAddress)
    {
        services.AddKeyedScoped<HttpClient>(_httpClientKey, (_, _) => new() { BaseAddress = new Uri(baseAddress) });

        services
            .AddSimplishAuthClientAsService(_httpClientKey)
            .AddScoped<IWeatherForecastClient, WeatherForecastClient>(sp =>
            {
                var httpClient = sp.GetRequiredKeyedService<HttpClient>(_httpClientKey);

                return new(httpClient);
            })
            .AddScoped<LedgerApiClient>();

        return services;
    }
}
