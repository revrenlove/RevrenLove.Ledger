using System.ComponentModel.DataAnnotations;

namespace RevrenLove.Ledger.Entities;

public class LedgerItem
{
    public required Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }

    public required Guid FinancialAccountHolderId { get; set; }

    public required decimal Amount { get; set; }

    public string? Memo { get; set; }

    [Timestamp]
    public DateTime CreatedOn { get; set; }

    public required FinancialAccount FinancialAccount { get; set; }
    public required FinancialAccountHolder FinancialAccountHolder { get; set; }
}
