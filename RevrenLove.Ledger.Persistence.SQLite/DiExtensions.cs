using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddSQLiteDbContext(this IServiceCollection services, string connectionString)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        services
            .AddDbContext<LedgerDbContext>(options =>
            {
                options.UseSqlite(connectionString, options =>
                {
                    options.MigrationsAssembly(assemblyName);
                });
            });

        return services;
    }
}
