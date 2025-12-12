using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class RecurringTransaction : IEntity, IActivable
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public required DateOnly StartDate { get; set; }
    public required Frequency Frequency { get; set; }
    public required bool IsActive { get; set; } = true;

    public FinancialAccount? FinancialAccount { get; set; }
}
