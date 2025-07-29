using RevrenLove.Ledger.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddLedgerServices(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IDataAccessor<>), typeof(DataAccessor<>));

        return services;
    }
}
