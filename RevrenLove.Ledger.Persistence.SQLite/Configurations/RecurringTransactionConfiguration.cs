using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class RecurringTransactionConfiguration : IEntityTypeConfiguration<RecurringTransaction>
{
    public void Configure(EntityTypeBuilder<RecurringTransaction> builder)
    {
        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .HasOne(rt => rt.FinancialAccount)
            .WithMany(fa => fa.RecurringOutgoingTransactionsTransactions)
            .HasForeignKey(rt => rt.FinancialAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rt => rt.DestinationFinancialAccount)
            .WithMany(fa => fa.RecurringIncomingTransactionsTransactions)
            .HasForeignKey(rt => rt.DestinationFinancialAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
