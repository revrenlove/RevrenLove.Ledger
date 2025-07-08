using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence;

public class LedgerDbContext(DbContextOptions<LedgerDbContext> options)
    : DbContext(options)
{
    public DbSet<FinancialAccount> FinancialAccounts { get; set; }
    public DbSet<FinancialAccountHolder> FinancialAccountHolders { get; set; }
    public DbSet<LedgerItem> LedgerItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // TODO: JE - This is where config shit will go if we need it. Delete this override if we don't need this.
        base.OnModelCreating(builder);
    }
}
