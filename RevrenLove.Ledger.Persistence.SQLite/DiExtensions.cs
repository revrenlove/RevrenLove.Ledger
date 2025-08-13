using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Persistence;
using RevrenLove.Ledger.Persistence.SQLite.Configurations;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddSQLiteDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<ILedgerDbContext, LedgerDbContext>(options =>
            {
                options.UseSqlite(connectionString, options =>
                {
                    options.MigrationsAssembly(typeof(LedgerItemConfiguration).Assembly.FullName);
                });
            });

        return services;
    }

}
