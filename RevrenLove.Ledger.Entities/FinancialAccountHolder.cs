namespace RevrenLove.Ledger.Entities;

public class FinancialAccountHolder
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<FinancialAccount> FinancialAccounts { get; } = new HashSet<FinancialAccount>();
    public ICollection<LedgerItem> LedgerItems { get; } = new HashSet<LedgerItem>();
}
