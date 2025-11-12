using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class RecurringTransaction : IActivable
{
    public Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }
    public Guid? AssociatedFinancialAccountId { get; set; }

    [Precision(10, 2)]
    public required decimal Amount { get; set; }

    public required DateOnly StartDate { get; set; }

    public required Frequency Frequency { get; set; }

    public string Description { get; set; } = default!;

    public required bool IsActive { get; set; } = true;


    public required FinancialAccount FinancialAccount { get; set; }
    public FinancialAccount? AssociatedFinancialAccount { get; set; }

    public ICollection<LedgerItem> LedgerItems { get; } = new HashSet<LedgerItem>();
}
