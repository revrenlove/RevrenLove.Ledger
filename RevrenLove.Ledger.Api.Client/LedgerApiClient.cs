using RevrenLove.SimplishAuth;

namespace RevrenLove.Ledger.Api.Client;

public class LedgerApiClient(
    WeatherForecastClient weatherForecastClient,
    ISimplishAuthClient simplishAuthClient
)
{
    public WeatherForecastClient WeatherForecast => weatherForecastClient;
    public ISimplishAuthClient SimplishAuthClient => simplishAuthClient;
}

