using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Persistence;
using RevrenLove.Ledger.Persistence.SqlServer.Configurations;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<ILedgerDbContext, LedgerDbContext>(options =>
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly(typeof(LedgerItemConfiguration).Assembly.FullName);
                });
            });

        return services;
    }

}
