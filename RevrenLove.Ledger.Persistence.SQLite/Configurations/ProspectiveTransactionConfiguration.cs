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
    }
}
