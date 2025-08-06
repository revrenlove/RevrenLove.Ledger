using RevrenLove.Ledger.Api;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddLedgerDatabase(this IServiceCollection services, ConfigurationManager configuration, string environment)
    {
        if (environment == LedgerEnvironments.Test.ToString())
        {
            Console.WriteLine("Jim Test");

            var connectionString = configuration.GetConnectionString("Test")!;

            services.AddSQLiteDbContext(connectionString);
        }
        else
        {
            var connectionString = configuration.GetConnectionString("Default")!;

            services.AddSqlServerDbContext(connectionString);
        }

        return services;
    }
}
