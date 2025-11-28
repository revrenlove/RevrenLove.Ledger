using RevrenLove.Ledger.Api.Client;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    private static string? _baseAddress;
    private static Action<IHttpClientBuilder>? _configure;

    public static IServiceCollection AddLedgerApiClient(this IServiceCollection services, string baseAddress, Action<IHttpClientBuilder>? configure = null)
    {
        _baseAddress = baseAddress.TrimEnd('/');
        _configure = configure;

        services
            .AddScoped<ILedgerApiClient, LedgerApiClient>()
            .AddSubClient<IWeatherForecastClient, WeatherForecastClient>()
            // Add More Sub Clients...
            .AddSimplishAuthClient(_baseAddress);

        return services;
    }

    private static IServiceCollection AddSubClient<TClient, TImplementation>(this IServiceCollection services)
        where TClient : class
        where TImplementation : class, TClient
    {
        var httpClientBuilder = services.AddHttpClient<TClient, TImplementation>(ConfigureClient<TImplementation>);

        _configure?.Invoke(httpClientBuilder);

        return services;
    }

    private static void ConfigureClient<T>(HttpClient client)
    {
        var baseAddress = _baseAddress ?? throw new InvalidOperationException("Base address is not set.");
        var resource = typeof(T).Name.Replace("Client", string.Empty);

        var uri = new Uri($"{baseAddress}/{resource}");

        client.BaseAddress = uri;
    }
}