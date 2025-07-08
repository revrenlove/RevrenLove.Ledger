using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<LedgerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            })
            .AddDbContextFactory<LedgerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Scoped);

        return services;
    }
}
