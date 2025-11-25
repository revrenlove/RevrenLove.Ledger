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
    }
}
