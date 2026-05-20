using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence.SqlServer;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddRevrenLedgerSqlServerDbContext(
        this IServiceCollection services,
        string connectionString)
    {
        services
            .AddDbContext<LedgerSqlServerDbContext>(options =>
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly(typeof(LedgerSqlServerDbContext).Assembly.FullName);
                });
            });

        return services;
    }
}
