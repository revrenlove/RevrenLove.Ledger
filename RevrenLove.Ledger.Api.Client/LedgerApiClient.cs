using RevrenLove.SimplishAuth.Client;

namespace RevrenLove.Ledger.Api.Client;

public class LedgerApiClient(
    IWeatherForecastClient weatherForecastClient,
    ISimplishAuthClient simplishAuthClient
)
{
    public IWeatherForecastClient WeatherForecast => weatherForecastClient;
    public ISimplishAuthClient SimplishAuthClient => simplishAuthClient;
}

