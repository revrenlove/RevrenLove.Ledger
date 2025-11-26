using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

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
    }
}
