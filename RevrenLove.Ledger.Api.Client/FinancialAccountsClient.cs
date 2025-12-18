using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface IFinancialAccountsClient : ILedgerApiSubClient<FinancialAccount>
{

}

internal class FinancialAccountsClient(HttpClient httpClient) : LedgerApiSubClient<FinancialAccount>(httpClient), IFinancialAccountsClient
{
    
}
