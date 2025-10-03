using RevrenLove.Ledger.Api.Client;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddLedgerApiClient(this IServiceCollection services, string baseAddress)
    {
        services
            .AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) })
            .AddScoped<WeatherForecastClient>()
            .AddScoped<LedgerApiClient>()
            .AddSimplishAuthClient();

        return services;
    }
}
