using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence.Configurations;

public class LedgerItemConfiguration : IEntityTypeConfiguration<LedgerItem>
{
    public void Configure(EntityTypeBuilder<LedgerItem> builder)
    {
        builder
            .Property(l => l.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder
           .Property(l => l.IsProjection)
           .HasDefaultValue(false);
    }
}
