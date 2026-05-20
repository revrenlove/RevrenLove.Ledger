using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.Configurations;

public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
{
    public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
    {
        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .Property(lt => lt.Description)
            .HasMaxLength(500);

        builder
            .HasOne(lt => lt.FinancialAccount)
            .WithMany(fa => fa.FinancialTransactions)
            .HasForeignKey(lt => lt.FinancialAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(lt => lt.FinancialAccount)
            .IsRequired(false);
    }
}
