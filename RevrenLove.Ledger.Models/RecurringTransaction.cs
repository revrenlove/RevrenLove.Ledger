using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Models;

public class RecurringTransaction
{
    public Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }

    public required Guid FinancialAccountHolderId { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly StartDate { get; set; }

    public required Frequency Frequency { get; set; }

    public string Description { get; set; } = default!;
}
