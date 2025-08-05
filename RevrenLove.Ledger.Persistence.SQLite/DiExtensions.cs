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
                // TODO: JE - Figure out how to do this with SQLite
                options.UseSqlite(connectionString);
            });

        return services;
    }

}
