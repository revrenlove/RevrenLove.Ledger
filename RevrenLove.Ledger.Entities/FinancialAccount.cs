using System.ComponentModel.DataAnnotations;

namespace RevrenLove.Ledger.Entities;

public class FinancialAccount : IEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    public required string FriendlyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// True if this account should be exempt from balance calculations (e.g., utilities, payroll, etc).
    /// </summary>
    public bool IsBalanceExempt { get; set; } = false;

    public LedgerUser? User { get; set; }

    public ICollection<FinancialTransaction> FinancialTransactions { get; } = new HashSet<FinancialTransaction>();
    public ICollection<RecurringTransaction> RecurringIncomingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
    public ICollection<RecurringTransaction> RecurringOutgoingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
}
