using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;
using System.Linq.Expressions;

namespace RevrenLove.Ledger.Persistence.SQLite;

public class LedgerSQLiteDbContext(DbContextOptions<LedgerSQLiteDbContext> options)
    : IdentityDbContext<LedgerUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<FinancialAccount> FinancialAccounts { get; set; }
    public DbSet<LedgerTransaction> LedgerTransactions { get; set; }
    public DbSet<ProspectiveTransaction> ProspectiveTransactions { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(LedgerSQLiteDbContext).Assembly);

        // Apply query filter to automatically exclude inactive records for all IActivable entities
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IActivable).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "x");
                var property = Expression.Property(parameter, nameof(IActivable.IsActive));
                var lambda = Expression.Lambda(property, parameter);

                entityType.SetQueryFilter(lambda);
            }
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        HandleSoftDelete();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        HandleSoftDelete();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void HandleSoftDelete()
    {
        var deletedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in deletedEntries)
        {
            if (entry.Entity is LedgerTransaction)
            {
                throw new InvalidOperationException("LedgerTransaction entities cannot be deleted. They are permanent records.");
            }

            if (entry.Entity is IActivable activable)
            {
                entry.State = EntityState.Modified;
                activable.IsActive = false;
            }
        }
    }
}
