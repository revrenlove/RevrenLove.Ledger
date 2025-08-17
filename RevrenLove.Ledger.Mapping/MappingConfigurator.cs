namespace RevrenLove.Ledger.Mapping;

public class MappingConfigurator
{
    private MappingConfigurator() { }

    private readonly List<IMappingDefinition> _mappingDefinitions = [];

    public void AddMapping<TSource, TDestination>(Func<TSource, TDestination> mappingFunction)
    {
        var mappingDefinition = new MappingDefinition<TSource, TDestination>(mappingFunction);

        _mappingDefinitions.Add(mappingDefinition);
    }

    public void AddMapping<TSource, TDestination>(
        Func<TSource, TDestination> mappingFunction,
        Func<TDestination, TSource> reverseMappingFunction)
    {
        AddMapping(mappingFunction);

        var mappingDefinition = new MappingDefinition<TDestination, TSource>(reverseMappingFunction);

        _mappingDefinitions.Add(mappingDefinition);
    }

    public IReadOnlyList<IMappingDefinition> GetMappingDefinitions() => _mappingDefinitions;

    public static MappingConfigurator Create() => new();
}
