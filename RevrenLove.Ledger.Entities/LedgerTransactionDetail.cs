namespace RevrenLove.Ledger.Entities;

public class LedgerTransactionDetail
{
    public Guid Id { get; set; }
    public Guid LedgerItemId { get; set; }
    public Guid? AssociatedFinancialAccountId { get; set; }
    public string? Notes { get; set; }

    public required LedgerTransaction LedgerItem { get; set; }
    public FinancialAccount? AssociatedFinancialAccount { get; set; }
}
