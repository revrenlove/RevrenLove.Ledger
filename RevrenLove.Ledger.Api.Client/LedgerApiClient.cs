using RevrenLove.SimplishAuth.Client;

namespace RevrenLove.Ledger.Api.Client;

public interface ILedgerApiClient
{
    IWeatherForecastClient WeatherForecast { get; }
    ISimplishAuthClient SimplishAuthClient { get; }
    IFinancialAccountsClient FinancialAccounts { get; }
}

internal class LedgerApiClient(
    IWeatherForecastClient weatherForecastClient,
    ISimplishAuthClient simplishAuthClient,
    IFinancialAccountsClient financialAccountsClient
) : ILedgerApiClient
{
    public IWeatherForecastClient WeatherForecast => weatherForecastClient;
    public ISimplishAuthClient SimplishAuthClient => simplishAuthClient;
    public IFinancialAccountsClient FinancialAccounts => financialAccountsClient;
}

