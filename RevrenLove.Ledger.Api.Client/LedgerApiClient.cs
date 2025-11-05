using RevrenLove.SimplishAuth;

namespace RevrenLove.Ledger.Api.Client;

internal class LedgerApiClient(
    IWeatherForecastClient weatherForecastClient,
    ISimplishAuthClient simplishAuthClient
) : ILedgerApiClient
{
    public IWeatherForecastClient WeatherForecast => weatherForecastClient;
    public ISimplishAuthClient SimplishAuth => simplishAuthClient;
}

public interface ILedgerApiClient
{
    IWeatherForecastClient WeatherForecast { get; }
    public ISimplishAuthClient SimplishAuth { get; }
}