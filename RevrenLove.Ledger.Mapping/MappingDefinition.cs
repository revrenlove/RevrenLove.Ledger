namespace RevrenLove.Ledger.Mapping;

public class MappingDefinition<TSource, TDestination>(Func<TSource, TDestination> mappingFunction) : IMappingDefinition
{
    public Delegate MappingDelegate => mappingFunction;

    public MappingSignature MappingSignature { get; } = new()
    {
        SourceType = typeof(TSource),
        DestinationType = typeof(TDestination),
    };
}
