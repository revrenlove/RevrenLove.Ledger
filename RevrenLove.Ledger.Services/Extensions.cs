using Microsoft.EntityFrameworkCore;

namespace RevrenLove.Ledger.Services;

internal static class Extensions
{
    public static IQueryable<Entities.RecurringTransaction> WithAccounts(this IQueryable<Entities.RecurringTransaction> queryable) =>
            queryable
                .Include(rt => rt.FinancialAccount)
                .Include(rt => rt.DestinationFinancialAccount);
}
