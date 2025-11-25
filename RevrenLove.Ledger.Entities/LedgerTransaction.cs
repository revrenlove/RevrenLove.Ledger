using Microsoft.EntityFrameworkCore;

namespace RevrenLove.Ledger.Entities;

public class LedgerTransaction
{
    public Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }

    [Precision(10, 2)]
    public required decimal Amount { get; set; }

    public string? Description { get; set; }

    public required DateOnly Date { get; set; }

    public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public required FinancialAccount FinancialAccount { get; set; }
}
