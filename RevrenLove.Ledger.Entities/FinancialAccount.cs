namespace RevrenLove.Ledger.Entities;

public class FinancialAccount : IEntity, IActivable
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool IsActive { get; set; } = true;

    public ICollection<FinancialAccountHolder> FinancialAccountHolders { get; } = new HashSet<FinancialAccountHolder>();
    public ICollection<LedgerItem> LedgerItems { get; } = new HashSet<LedgerItem>();
}
