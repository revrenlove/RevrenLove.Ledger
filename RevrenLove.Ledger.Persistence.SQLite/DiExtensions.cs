using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence.SQLite;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddRevrenLedgerSQLiteDbContext(
        this IServiceCollection services,
        string connectionString)
    {
        services
            .AddDbContext<LedgerSQLiteDbContext>(options =>
            {
                options.UseSqlite(connectionString, options =>
                {
                    options.MigrationsAssembly(typeof(LedgerSQLiteDbContext).Assembly.FullName);
                });
            });

        return services;
    }
}
