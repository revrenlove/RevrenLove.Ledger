using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Mapping;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddLedgerMappings(this IServiceCollection services, Action<MappingConfigurator> cfg)
    {
        var configurator = MappingConfigurator.Create();

        cfg(configurator);

        Mapper mapper = new(configurator.GetMappingDefinitions());

        services.AddSingleton<IMapper>(mapper);

        return services;
    }
}
