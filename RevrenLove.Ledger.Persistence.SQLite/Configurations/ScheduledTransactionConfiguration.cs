using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class ScheduledTransactionConfiguration : IEntityTypeConfiguration<ScheduledTransaction>
{
    public void Configure(EntityTypeBuilder<ScheduledTransaction> builder)
    {
        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .HasOne(pt => pt.FinancialAccount)
            .WithMany(fa => fa.ScheduledOutgoingTransactions)
            .HasForeignKey(pt => pt.FinancialAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pt => pt.DestinationFinancialAccount)
            .WithMany(fa => fa.ScheduledIncomingTransactions)
            .HasForeignKey(pt => pt.DestinationFinancialAccountId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}
