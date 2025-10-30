namespace RevrenLove.Ledger.Api.Client;

public interface ILedgerApiHttpClient
{
    HttpClient Instance { get; }
}

internal class LedgerApiHttpClient(HttpClient httpClient) : ILedgerApiHttpClient
{
    public HttpClient Instance => httpClient;
}
