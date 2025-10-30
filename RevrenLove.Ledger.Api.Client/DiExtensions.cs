using RevrenLove.Ledger.Api.Client;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddLedgerApiClient(this IServiceCollection services, string baseAddress)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };

        services
            .AddScoped<ILedgerApiHttpClient>(_ => new LedgerApiHttpClient(httpClient))
            .AddScoped<IWeatherForecastClient, WeatherForecastClient>()
            .AddScoped<ILedgerApiClient, LedgerApiClient>()
            .AddSimplishAuthClient(httpClient);

        return services;
    }
}
