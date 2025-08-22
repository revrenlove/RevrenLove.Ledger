using System.Net.Http.Json;
using RevrenLove.Ledger.Web.Pages;

namespace RevrenLove.Ledger.Web;

public class LedgerApiClient(FinancialAccountClient financialAccountClient)
{
    public FinancialAccountClient FinancialAccounts => financialAccountClient;
}

public class FinancialAccountClient(HttpClient httpClient)
{
    private static readonly string _resource = "FinancialAccount";

    public async Task<FinancialAccount[]> Get() =>
        await
            httpClient
                .GetFromJsonAsync<FinancialAccount[]>(_resource) ?? [];
}
