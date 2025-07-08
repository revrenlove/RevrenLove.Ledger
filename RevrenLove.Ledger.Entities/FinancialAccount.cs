namespace RevrenLove.Ledger.Entities;

public class FinancialAccount
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<FinancialAccountHolder> FinancialAccountHolders { get; } = new HashSet<FinancialAccountHolder>();
    public ICollection<LedgerItem> LedgerItems { get; } = new HashSet<LedgerItem>();
}
