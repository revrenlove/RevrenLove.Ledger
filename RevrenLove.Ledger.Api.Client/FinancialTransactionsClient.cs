using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IFinancialTransactionsClient
{
    Task<ApiClientResult<FinancialTransaction>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<ApiClientResult<List<FinancialTransaction>>> GetAsync(CancellationToken cancellationToken = default);
    Task<ApiClientResult<List<FinancialTransaction>>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
    Task<ApiClientResult<FinancialTransaction>> CreateAsync(CreateFinancialTransactionRequest createFinancialTransactionRequest, CancellationToken cancellationToken = default);
    Task<ApiClientResult<FinancialTransaction>> UpdateAsync(FinancialTransaction financialTransaction, CancellationToken cancellationToken = default);
    Task<ApiClientResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

internal class FinancialTransactionsClient(HttpClient httpClient)
    : LedgerApiSubClient<FinancialTransaction>(httpClient), IFinancialTransactionsClient
{
    public async Task<ApiClientResult<List<FinancialTransaction>>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"?{nameof(financialAccountId)}={financialAccountId}", cancellationToken);

        var result = await ApiClientResult<List<FinancialTransaction>>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult<FinancialTransaction>> CreateAsync(CreateFinancialTransactionRequest createFinancialTransactionRequest, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync(createFinancialTransactionRequest, cancellationToken);

        var result = await ApiClientResult<FinancialTransaction>.CreateAsync(response);

        return result;
    }
}