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
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var sourceType = typeof(TSource);

        if (sourceType == typeof(object))
        {
            sourceType = source.GetType();
        }

        var mappingTypes = new MappingSignature
        {
            SourceType = sourceType,
            DestinationType = typeof(TDestination)
        };

        if (!_mappingFunctionsByMappingTypes.TryGetValue(mappingTypes, out var del))
        {
            var msg = $"No mapping configured for {sourceType.FullName} to {typeof(TDestination).FullName}";

            throw new InvalidOperationException(msg);
        }

        TDestination destination;

        if (typeof(TSource) == typeof(object))
        {
            destination = (TDestination)del.DynamicInvoke(source!)!;
        }
        else
        {
            var mappingFunction = (Func<TSource, TDestination>)del;

            destination = mappingFunction(source);
        }

        return destination;
    }
}
