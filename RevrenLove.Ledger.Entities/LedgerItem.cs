using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RevrenLove.Ledger.Entities;

public class LedgerItem : IEntity, IActivable
{
    public required Guid Id { get; set; }

    public required Guid FinancialAccountId { get; set; }

    public required Guid FinancialAccountHolderId { get; set; }

    public Guid? RecurringTransactionId { get; set; }

    [Precision(10, 2)]
    public required decimal Amount { get; set; }

    public string? Memo { get; set; }

    [Timestamp]
    public DateTime CreatedOn { get; set; }
    public required bool IsActive { get; set; } = true;

    public required FinancialAccount FinancialAccount { get; set; }

    public required FinancialAccountHolder FinancialAccountHolder { get; set; }

    public RecurringTransaction? RecurringTransaction { get; set; }
}
