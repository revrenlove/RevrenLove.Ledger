namespace RevrenLove.Ledger.Api.Client;

public class LedgerApiClient(
    WeatherForecastClient weatherForecastClient
)
{
    public WeatherForecastClient WeatherForecast => weatherForecastClient;
}

