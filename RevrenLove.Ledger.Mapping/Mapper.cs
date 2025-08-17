using RevrenLove.Ledger.Abstractions;

namespace RevrenLove.Ledger.Mapping;

public class Mapper : IMapper
{
    private static Mapper? _instance;

    private readonly Dictionary<MappingSignature, Delegate> _mappingFunctionsByMappingTypes = [];

    internal Mapper(IEnumerable<IMappingDefinition> mappingDefinitions)
    {
        if (_instance is not null)
        {
            throw new InvalidOperationException("Mapper has already been initialized. Only initialize this ONCE!");
        }

        _mappingFunctionsByMappingTypes = mappingDefinitions.ToDictionary(x => x.MappingSignature, x => x.MappingDelegate);

        _instance = this;
    }

    public TDestination Map<TDestination>(object source) =>
        MapInternal<object, TDestination>(source);

    public TDestination Map<TDestination, TSource>(TSource source) =>
        MapInternal<TSource, TDestination>(source);

    public static IMapper GetInstance() =>
        _instance ??
            throw new InvalidOperationException("Mapper has not been initialized yet.");

    private TDestination MapInternal<TSource, TDestination>(TSource source)
    {
        var mappingTypes = new MappingSignature
        {
            SourceType = typeof(TSource),
            DestinationType = typeof(TDestination)
        };

        if (!_mappingFunctionsByMappingTypes.TryGetValue(mappingTypes, out var del))
        {
            throw new InvalidOperationException(
                $"No mapping configured for {typeof(TSource).FullName} to {typeof(TDestination).FullName}"
            );
        }

        var mappingFunction = (Func<TSource, TDestination>)del;
        return mappingFunction(source);
    }
}
