using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
{
    public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
    {
        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .HasOne(lt => lt.FinancialAccount)
            .WithMany(fa => fa.FinancialTransactions)
            .HasForeignKey(lt => lt.FinancialAccountId)
            .IsRequired();

        builder
            .Navigation(lt => lt.FinancialAccount)
            .IsRequired(false);
    }
}
