using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite;

public class LedgerSQLiteDbContext(DbContextOptions<LedgerSQLiteDbContext> options)
    : IdentityDbContext<LedgerUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<FinancialAccount> FinancialAccounts { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    public DbSet<RunningBalance> RunningBalances { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(LedgerSQLiteDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await UpdateRunningBalancesAsync(cancellationToken);

            result += await base.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    private async Task<int> UpdateRunningBalancesAsync(CancellationToken cancellationToken)
    {
        var trackedTransactions =
                ChangeTracker.Entries<FinancialTransaction>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified)
                .ToList();

        var earliestAffectedDateByAccountId =
            trackedTransactions
                .SelectMany(entry =>
                {
                    var accountId = entry.State == EntityState.Deleted
                        ? entry.OriginalValues.GetValue<Guid>(nameof(FinancialTransaction.FinancialAccountId))
                        : entry.Entity.FinancialAccountId;

                    var dates = new List<DateOnly>();

                    if (entry.State == EntityState.Added)
                    {
                        dates.Add(entry.Entity.Date);
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        dates.Add(entry.OriginalValues.GetValue<DateOnly>(nameof(FinancialTransaction.Date)));
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        dates.Add(entry.Entity.Date);
                        dates.Add(entry.OriginalValues.GetValue<DateOnly>(nameof(FinancialTransaction.Date)));
                    }

                    return dates.Select(date => (AccountId: accountId, Date: date));
                })
                .GroupBy(x => x.AccountId)
                .ToDictionary(g => g.Key, g => g.Min(x => x.Date));

        trackedTransactions
            .Where(t => t.State == EntityState.Added)
            .ToList()
            .ForEach(e =>
            {
                var runningBalance = new RunningBalance
                {
                    Id = Guid.NewGuid(),
                    FinancialTransactionId = e.Entity.Id,
                    Balance = 0
                };

                RunningBalances.Add(runningBalance);
            });

        var affectedRows = await base.SaveChangesAsync(cancellationToken);

        if (earliestAffectedDateByAccountId.Count > 0)
        {
            var allAccountIds = earliestAffectedDateByAccountId.Keys.ToList();

            var allTransactions = await FinancialTransactions
                .Include(t => t.RunningBalance)
                .Where(t => allAccountIds.Contains(t.FinancialAccountId))
                .OrderBy(t => t.FinancialAccountId)
                .ThenBy(t => t.Date)
                .ThenByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToListAsync(cancellationToken);

            foreach (var (accountId, earliestDate) in earliestAffectedDateByAccountId)
            {
                var accountTransactions = allTransactions
                    .Where(t => t.FinancialAccountId == accountId)
                    .ToList();

                var previousBalance = accountTransactions
                    .Where(t => t.Date < earliestDate)
                    .OrderByDescending(t => t.Date)
                    .ThenBy(t => t.Amount)
                    .ThenByDescending(t => t.Id)
                    .Select(t => t.RunningBalance!.Balance)
                    .FirstOrDefault();

                var transactionsToUpdate = accountTransactions
                    .Where(t => t.Date >= earliestDate)
                    .OrderBy(t => t.Date)
                    .ThenByDescending(t => t.Amount)
                    .ThenBy(t => t.Id)
                    .ToList();

                var runningBalance = previousBalance;

                foreach (var transaction in transactionsToUpdate)
                {
                    runningBalance += transaction.Amount;
                    transaction.RunningBalance!.Balance = runningBalance;
                }
            }

            affectedRows += await base.SaveChangesAsync(cancellationToken);
        }

        return affectedRows;
    }

    #region `SaveChanges()` Override
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    private const string SaveChangesErrorMessage = "SaveChanges is not supported. Use SaveChangesAsync instead.";

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete(SaveChangesErrorMessage, error: true)]
    public override int SaveChanges()
    {
        throw new NotSupportedException(SaveChangesErrorMessage);
    }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
    #endregion
}
