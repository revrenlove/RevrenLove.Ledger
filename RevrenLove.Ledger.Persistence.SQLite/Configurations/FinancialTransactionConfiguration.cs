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
            .Property(lt => lt.Description)
            .HasMaxLength(500);

        builder
            .ToTable(t => t.HasCheckConstraint("CK_FinancialTransaction_Description_MinLength", "LENGTH(TRIM(Description)) >= 1"));

        builder
            .HasOne(lt => lt.FinancialAccount)
            .WithMany(fa => fa.FinancialTransactions)
            .HasForeignKey(lt => lt.FinancialAccountId)
            .IsRequired();

        builder
            .Navigation(lt => lt.FinancialAccount)
            .IsRequired(false);

        //builder
        //    .Property(lt => lt.ComputedDisplayValue)
        //    .HasComputedColumnSql("strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id", stored: true);
    }
}
