using RevrenLove.Ledger.Api;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

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
