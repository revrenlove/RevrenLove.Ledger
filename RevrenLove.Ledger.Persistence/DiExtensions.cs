using Microsoft.AspNetCore.Identity;
using RevrenLove.Ledger.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IdentityBuilder AddLedgerEntityFrameworkStores(this IdentityBuilder builder)
    {
        builder.AddEntityFrameworkStores<LedgerDbContext>();

        return builder;
    }
}
