using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite;

public class LedgerSQLiteDbContext(DbContextOptions<LedgerSQLiteDbContext> options)
    : LedgerDbContext(options)
{
    protected override ModelBuilder ApplyProviderSpecificConfigurations(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint("CK_FinancialTransaction_Description_MinLength", "LENGTH(TRIM(Description)) >= 1"));
        });
}
