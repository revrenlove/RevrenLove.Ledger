using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SqlServer;

public class LedgerSqlServerDbContext(DbContextOptions<LedgerSqlServerDbContext> options)
    : LedgerDbContext(options)
{
    protected override ModelBuilder ApplyProviderSpecificConfigurations(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint("CK_FinancialTransaction_Description_MinLength", "LEN(TRIM(Description)) >= 1"));
        });
}
