namespace RevrenLove.Ledger.Services.Models.AccountTypes;

public record DepositAccount : IFinancialAccount
{
    public Guid Id { get; set; }
    public Guid MetadataId { get; set; }
    public string FriendlyId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string? AccountNumber { get; set; }
    public string? RoutingNumber { get; set; }
}
