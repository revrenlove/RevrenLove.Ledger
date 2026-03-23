using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class RunningBalanceConfiguration : IEntityTypeConfiguration<RunningBalance>
{
    public void Configure(EntityTypeBuilder<RunningBalance> builder)
    {
        builder
            .Property(rb => rb.Balance)
            .HasPrecision(10, 2);

        builder
            .HasOne(rb => rb.FinancialTransaction)
            .WithOne(ft => ft.RunningBalance)
            .HasForeignKey<RunningBalance>(rb => rb.FinancialTransactionId)
            .IsRequired();

        builder
            .HasIndex(rb => rb.FinancialTransactionId)
            .IsUnique();
    }
}
