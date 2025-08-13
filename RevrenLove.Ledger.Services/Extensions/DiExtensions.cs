using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Services;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddLedgerServices(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IDataAccessor<>), typeof(DataAccessor<>));

        return services;
    }
}
