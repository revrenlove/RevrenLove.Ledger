namespace RevrenLove.Ledger.Entities;

public class FinancialAccount : IActivable
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool IsActive { get; set; } = true;

    public required LedgerUser User { get; set; }

    public ICollection<LedgerTransaction> LedgerTransactions { get; } = new HashSet<LedgerTransaction>();
    public ICollection<RecurringTransaction> RecurringTransactions { get; } = new HashSet<RecurringTransaction>();
    public ICollection<ProspectiveTransaction> ProspectiveTransactions { get; } = new HashSet<ProspectiveTransaction>();
}
