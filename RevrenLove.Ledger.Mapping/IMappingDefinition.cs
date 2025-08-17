namespace RevrenLove.Ledger.Mapping;

public interface IMappingDefinition
{
    MappingSignature MappingSignature { get; }
    Delegate MappingDelegate { get; }
}
