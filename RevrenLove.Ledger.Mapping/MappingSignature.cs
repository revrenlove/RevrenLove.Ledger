namespace RevrenLove.Ledger.Mapping;

public readonly struct MappingSignature
{
    public required readonly Type SourceType { get; init; }
    public required readonly Type DestinationType { get; init; }
}
