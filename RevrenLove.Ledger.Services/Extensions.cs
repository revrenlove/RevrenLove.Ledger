using Microsoft.EntityFrameworkCore;

namespace RevrenLove.Ledger.Services;

internal static class Extensions
{
    public static IQueryable<Entities.RecurringTransaction> WithAccounts(this IQueryable<Entities.RecurringTransaction> queryable) =>
            queryable
                .Include(rt => rt.FinancialAccount)
                .Include(rt => rt.DestinationFinancialAccount);

    public static IOrderedQueryable<Entities.FinancialTransaction> ApplyDefaultOrdering(this IQueryable<Entities.FinancialTransaction> query) =>
        query
            .OrderBy(t => t.Date)
            .ThenByDescending(t => t.Amount)
            .ThenBy(t => t.Id);

    public static async Task<Entities.IFinancialAccountTypeEntity> GetFinancialAccountTypeEntity<T>(this IDataAccessor<T> financialAccountTypeEntities, Guid financialAccountId, CancellationToken cancellationToken = default)
        where T : class, Entities.IFinancialAccountTypeEntity, Entities.IEntity
    {
        var financialAccountTypeEntity =
            await 
                financialAccountTypeEntities
                    .Include(e => e.FinancialAccount)
                    .Where(a => a.FinancialAccountId == financialAccountId)
                    .FirstOrDefaultAsync(cancellationToken) ??
                        throw new KeyNotFoundException($"Financial account with ID '{financialAccountId}' was not found.");
        
        return financialAccountTypeEntity;
    }
}
