namespace RevrenLove.Ledger.Entities;

public class FinancialAccount : IEntity, IActivable
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string FriendlyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// True if this account should be exempt from balance calculations (e.g., utilities, payroll, etc).
    /// </summary>
    public bool IsBalanceExempt { get; set; } = false;
    public required bool IsActive { get; set; } = true;

    public LedgerUser? User { get; set; }

    public ICollection<LedgerTransaction> LedgerTransactions { get; } = new HashSet<LedgerTransaction>();
    public ICollection<RecurringTransaction> RecurringIncomingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
    public ICollection<RecurringTransaction> RecurringOutgoingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
    public ICollection<ProspectiveTransaction> ProspectiveIncomingTransactions { get; } = new HashSet<ProspectiveTransaction>();
    public ICollection<ProspectiveTransaction> ProspectiveOutgoingTransactions { get; } = new HashSet<ProspectiveTransaction>();
}
