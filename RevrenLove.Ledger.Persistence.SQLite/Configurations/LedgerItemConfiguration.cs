using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.SQLite.Configurations;

public class LedgerItemConfiguration : IEntityTypeConfiguration<LedgerItem>
{
    public void Configure(EntityTypeBuilder<LedgerItem> builder)
    {
        builder
            .Property(l => l.CreatedOn)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
           .Property(l => l.IsProjection)
           .HasDefaultValue(false);
    }
}
