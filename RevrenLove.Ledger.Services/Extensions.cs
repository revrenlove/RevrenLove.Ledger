using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

internal static class Extensions
{
    public static IQueryable<Entities.RecurringTransaction> WithAccounts(this IQueryable<Entities.RecurringTransaction> queryable) =>
            queryable
                .Include(rt => rt.FinancialAccount)
                .Include(rt => rt.DestinationFinancialAccount);

    public static IOrderedQueryable<FinancialTransaction> ApplyDefaultOrdering(this IQueryable<FinancialTransaction> query) =>
        query
            .OrderBy(t => t.Date)
            .ThenByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
}
