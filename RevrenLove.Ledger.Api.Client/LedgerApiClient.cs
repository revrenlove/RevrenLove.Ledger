using RevrenLove.SimplishAuth.Client;

namespace RevrenLove.Ledger.Api.Client;

public interface ILedgerApiClient
{
    IWeatherForecastClient WeatherForecast { get; }
    ISimplishAuthClient SimplishAuthClient { get; }
}

internal class LedgerApiClient(
    IWeatherForecastClient weatherForecastClient,
    ISimplishAuthClient simplishAuthClient
) : ILedgerApiClient
{
    public IWeatherForecastClient WeatherForecast => weatherForecastClient;
    public ISimplishAuthClient SimplishAuthClient => simplishAuthClient;
}

