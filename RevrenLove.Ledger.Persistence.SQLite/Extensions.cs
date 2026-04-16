using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite;

public static class Extensions
{
    public static IOrderedQueryable<FinancialTransaction> OrderByTruth(this IQueryable<FinancialTransaction> query) =>
        query
            .OrderBy(t => t.Date)
            .ThenByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
}
