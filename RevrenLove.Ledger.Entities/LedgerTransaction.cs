namespace RevrenLove.Ledger.Entities;

public class LedgerTransaction
{
    public required Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly Date { get; set; }
    public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public FinancialAccount? FinancialAccount { get; set; }
}
