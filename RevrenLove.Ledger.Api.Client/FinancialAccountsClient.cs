using System.Net.Http.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IFinancialAccountsClient
{
    Task<ApiClientResult<FinancialAccount>> GetAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
}

internal class FinancialAccountsClient(HttpClient httpClient) : IFinancialAccountsClient
{
    public async Task<ApiClientResult<FinancialAccount>> GetAsync(
        Guid financialAccountId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(financialAccountId.ToString(), cancellationToken);

        var result = await ApiClientResult<FinancialAccount>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult<IEnumerable<FinancialAccount>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(cancellationToken: cancellationToken);
        
        var result = await ApiClientResult<IEnumerable<FinancialAccount>>.CreateAsync(response);
        
        return result;
    }

    public async Task<ApiClientResult<FinancialAccount>> CreateAsync(
        FinancialAccount financialAccount,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(financialAccount, cancellationToken);
        
        var result = await ApiClientResult<FinancialAccount>.CreateAsync(response);
        
        return result;
    }

    public async Task<ApiClientResult<FinancialAccount>> UpdateAsync(
        FinancialAccount financialAccount,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(financialAccount.Id.ToString(), financialAccount, cancellationToken);
        
        var result = await ApiClientResult<FinancialAccount>.CreateAsync(response);
        
        return result;
    }

    public async Task<ApiClientResult> DeleteAsync(
        Guid financialAccountId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(financialAccountId.ToString(), cancellationToken);
        
        var result = await ApiClientResult.CreateAsync(response);
        
        return result;
    }
}
