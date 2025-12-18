using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class ProspectiveTransactionConfiguration : IEntityTypeConfiguration<ProspectiveTransaction>
{
    public void Configure(EntityTypeBuilder<ProspectiveTransaction> builder)
    {
        builder
            .Property(l => l.Amount)
            .HasPrecision(10, 2);

        builder
            .HasOne(pt => pt.FinancialAccount)
            .WithMany(fa => fa.ProspectiveOutgoingTransactions)
            .HasForeignKey(pt => pt.FinancialAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pt => pt.DestinationFinancialAccount)
            .WithMany(fa => fa.ProspectiveIncomingTransactions)
            .HasForeignKey(pt => pt.DestinationFinancialAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
