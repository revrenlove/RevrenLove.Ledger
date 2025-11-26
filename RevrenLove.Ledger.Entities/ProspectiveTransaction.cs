using Microsoft.EntityFrameworkCore;

namespace RevrenLove.Ledger.Entities;

public class ProspectiveTransaction
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public required bool IsActive { get; set; } = true;

    public required FinancialAccount FinancialAccount { get; set; }
}
