namespace RevrenLove.Ledger.Mapping;

public class MappingConfigurator
{
    private MappingConfigurator() { }

    private readonly List<IMappingDefinition> _mappingDefinitions = [];

    public void AddMapping<TSource, TDestination>(Func<TSource, TDestination> mappingFunction)
    {
        var mappingDefinition = new MappingDefinition<TSource, TDestination>(mappingFunction);

        AddMapping(mappingDefinition);
    }

    public void AddMapping<TSource, TDestination>(MappingDefinition<TSource, TDestination> mappingDefinition)
    {
        _mappingDefinitions.Add(mappingDefinition);
    }

    public void AddMapping<TSource, TDestination>(
        Func<TSource, TDestination> mappingFunction,
        Func<TDestination, TSource> reverseMappingFunction)
    {
        AddMapping(mappingFunction);
        AddMapping(reverseMappingFunction);
    }

    public void AddMapping<TSource, TDestination>(
        MappingDefinition<TSource, TDestination> mappingDefinition,
        MappingDefinition<TDestination, TSource> reverseMappingDefinition)
    {
        AddMapping(mappingDefinition);
        AddMapping(reverseMappingDefinition);
    }

    public IReadOnlyList<IMappingDefinition> GetMappingDefinitions() => _mappingDefinitions;

    public static MappingConfigurator Create() => new();
}
