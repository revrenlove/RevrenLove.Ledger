using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Persistence;
using RevrenLove.Ledger.Persistence.SQLite.Configurations;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

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
