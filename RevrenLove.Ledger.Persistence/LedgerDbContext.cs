using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence;

public class LedgerDbContext(DbContextOptions<LedgerDbContext> options)
    : DbContext(options), ILedgerDbContext
{
    public DbSet<FinancialAccount> FinancialAccounts { get; set; }
    public DbSet<FinancialAccountHolder> FinancialAccountHolders { get; set; }
    public DbSet<LedgerItem> LedgerItems { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureActivableEntities();

        builder.ApplyConfigurationsFromAssembly(typeof(LedgerDbContext).Assembly);
    }
}
