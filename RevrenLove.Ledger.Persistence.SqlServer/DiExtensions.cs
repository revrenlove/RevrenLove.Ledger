using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<ILedgerDbContext, LedgerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

        return services;
    }

}
