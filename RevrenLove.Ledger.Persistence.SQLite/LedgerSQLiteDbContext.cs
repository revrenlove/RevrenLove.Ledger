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
        await UpdateRunningBalancesAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateRunningBalancesAsync(CancellationToken cancellationToken)
    {
        var affectedAccountIds = new HashSet<Guid>();

        var trackedTransactions = ChangeTracker.Entries<FinancialTransaction>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in trackedTransactions)
        {
            var accountId = entry.State == EntityState.Deleted
                ? entry.OriginalValues.GetValue<Guid>(nameof(FinancialTransaction.FinancialAccountId))
                : entry.Entity.FinancialAccountId;

            if (entry.State == EntityState.Added)
            {
                affectedAccountIds.Add(accountId);

                var runningBalance = new RunningBalance
                {
                    Id = Guid.NewGuid(),
                    FinancialTransactionId = entry.Entity.Id,
                    Balance = 0
                };
                RunningBalances.Add(runningBalance);
            }
            else if (entry.State == EntityState.Deleted)
            {
                affectedAccountIds.Add(accountId);

                var runningBalance = await RunningBalances
                    .FirstOrDefaultAsync(rb => rb.FinancialTransactionId == entry.Entity.Id, cancellationToken);

                if (runningBalance is not null)
                {
                    RunningBalances.Remove(runningBalance);
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                var amountChanged = entry.Property(nameof(FinancialTransaction.Amount)).IsModified;
                if (amountChanged)
                {
                    affectedAccountIds.Add(accountId);
                }
            }
        }

        foreach (var accountId in affectedAccountIds)
        {
            await RecalculateRunningBalancesForAccountAsync(accountId, cancellationToken);
        }
    }

    private async Task RecalculateRunningBalancesForAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var affectedEntries = ChangeTracker.Entries<FinancialTransaction>()
            .Where(e => (e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified) &&
                        (e.State == EntityState.Deleted 
                            ? e.OriginalValues.GetValue<Guid>(nameof(FinancialTransaction.FinancialAccountId))
                            : e.Entity.FinancialAccountId) == accountId)
            .ToList();

        if (affectedEntries.Count == 0)
        {
            return;
        }

        var candidateTransactions = new List<(DateOnly Date, decimal Amount, Guid Id)>();

        foreach (var entry in affectedEntries)
        {
            candidateTransactions.Add((entry.Entity.Date, entry.Entity.Amount, entry.Entity.Id));

            if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                var oldDate = entry.OriginalValues.GetValue<DateOnly>(nameof(FinancialTransaction.Date));
                var oldAmount = entry.OriginalValues.GetValue<decimal>(nameof(FinancialTransaction.Amount));
                candidateTransactions.Add((oldDate, oldAmount, entry.Entity.Id));
            }
        }

        var (Date, Amount, Id) = candidateTransactions
            .OrderBy(t => t.Date)
            .ThenBy(t => t.Amount)
            .ThenBy(t => t.Id)
            .First();

        var previousBalance = await FinancialTransactions
            .Include(t => t.RunningBalance)
            .Where(t => t.FinancialAccountId == accountId &&
                       (t.Date < Date ||
                        (t.Date == Date && t.Amount < Amount) ||
                        (t.Date == Date && t.Amount == Amount && t.Id.CompareTo(Id) < 0)))
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Amount)
            .ThenByDescending(t => t.Id)
            .Select(t => t.RunningBalance!.Balance)
            .FirstOrDefaultAsync(cancellationToken);

        await FinancialTransactions
            .Include(t => t.RunningBalance)
            .Where(t => t.FinancialAccountId == accountId &&
                       (t.Date > Date ||
                        (t.Date == Date && t.Amount > Amount) ||
                        (t.Date == Date && t.Amount == Amount && t.Id.CompareTo(Id) >= 0)))
            .LoadAsync(cancellationToken);

        var transactionsToUpdate = ChangeTracker.Entries<FinancialTransaction>()
            .Where(e => e.State != EntityState.Deleted &&
                        e.Entity.FinancialAccountId == accountId &&
                        (e.Entity.Date > Date ||
                         (e.Entity.Date == Date && e.Entity.Amount > Amount) ||
                         (e.Entity.Date == Date && e.Entity.Amount == Amount && e.Entity.Id.CompareTo(Id) >= 0)))
            .Select(e => e.Entity)
            .OrderBy(t => t.Date)
            .ThenBy(t => t.Amount)
            .ThenBy(t => t.Id)
            .ToList();

        decimal runningTotal = previousBalance;

        foreach (var transaction in transactionsToUpdate)
        {
            runningTotal += transaction.Amount;

            transaction.RunningBalance!.Balance = runningTotal;
        }
    }

    #region SaveChanges Override
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
