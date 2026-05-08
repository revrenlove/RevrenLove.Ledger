using System.ComponentModel.DataAnnotations;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class FinancialAccount : IEntity
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public required string FriendlyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public FinancialAccountType AccountType { get; set; }

    public LedgerUser? User { get; set; }

    public ICollection<FinancialTransaction> FinancialTransactions { get; } = new HashSet<FinancialTransaction>();
    public ICollection<RecurringTransaction> RecurringIncomingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
    public ICollection<RecurringTransaction> RecurringOutgoingTransactionsTransactions { get; } = new HashSet<RecurringTransaction>();
}
