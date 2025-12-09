using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class RecurringTransaction : IActivable
{
    public required Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public required DateOnly StartDate { get; set; }
    public required Frequency Frequency { get; set; }
    public required bool IsActive { get; set; } = true;

    public FinancialAccount? FinancialAccount { get; set; }
}
