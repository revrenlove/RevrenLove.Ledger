using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

// TODO: JE - This will all get refactored into the appropriate spot...
internal class CatchAllService(
    IDataAccessor<FinancialAccount> financialAccounts,
    IDataAccessor<FinancialAccountHolder> financialAccountHolders,
    IDataAccessor<LedgerItem> ledgerItems,
    IDataAccessor<RecurringTransaction> recurringTransactions,
    ILedgerDbContext dbContext
)
{
    // Get balance for account
    public async Task<decimal> GetBalanceForAccount(Guid accountId) =>
        await
            dbContext
                .LedgerItems
                .Where(l => l.FinancialAccountId == accountId && !l.IsProjection)
                .SumAsync(x => x.Amount);
}
