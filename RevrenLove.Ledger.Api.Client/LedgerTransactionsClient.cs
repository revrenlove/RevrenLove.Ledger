using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface ILedgerTransactionsClient
{
    Task<ApiClientResult<LedgerTransaction>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiClientResult<List<LedgerTransaction>>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
    Task<ApiClientResult<LedgerTransaction>> CreateAsync(LedgerTransaction model, CancellationToken cancellationToken = default);
}

internal class LedgerTransactionsClient(HttpClient httpClient) : LedgerApiSubClient<LedgerTransaction>(httpClient), ILedgerTransactionsClient
{
    public async Task<ApiClientResult<List<LedgerTransaction>>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"?{nameof(financialAccountId)}={financialAccountId}", cancellationToken);

        var result = await ApiClientResult<List<LedgerTransaction>>.CreateAsync(response);
        
        return result;
    }
}
