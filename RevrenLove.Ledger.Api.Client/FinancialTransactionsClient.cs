using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IFinancialTransactionsClient
{
    Task<ApiClientResult<List<FinancialTransaction>>> GetAsync(CancellationToken cancellationToken = default);
}

internal class FinancialTransactionsClient(HttpClient httpClient) : LedgerApiSubClient<FinancialTransaction>(httpClient), IFinancialTransactionsClient
{
}
