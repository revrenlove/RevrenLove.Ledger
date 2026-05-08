using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public abstract record FinancialAccount
{
    public Guid Id { get; set; }
    public required string FriendlyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
