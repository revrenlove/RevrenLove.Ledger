using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class LedgerTransactionConfiguration : IEntityTypeConfiguration<LedgerTransaction>
{
    public void Configure(EntityTypeBuilder<LedgerTransaction> builder)
    {
        builder
            .Property(l => l.CreatedOn)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .HasOne(lt => lt.FinancialAccount)
            .WithMany(fa => fa.LedgerTransactions)
            .HasForeignKey(lt => lt.FinancialAccountId)
            .IsRequired();

        builder
            .Navigation(lt => lt.FinancialAccount)
            .IsRequired(false);
    }
}
