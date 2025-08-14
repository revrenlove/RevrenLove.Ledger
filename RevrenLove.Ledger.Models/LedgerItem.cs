namespace RevrenLove.Ledger.Models;

public class LedgerItem
{
    public Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }

    public required Guid FinancialAccountHolderId { get; set; }

    public Guid? RecurringTransactionId { get; set; }

    public required decimal Amount { get; set; }

    public string? Memo { get; set; }

    public required DateOnly Date { get; set; }

    public required bool IsProjection { get; set; } = false;

    public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
