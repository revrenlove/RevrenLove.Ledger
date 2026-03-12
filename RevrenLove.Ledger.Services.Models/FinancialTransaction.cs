using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public record FinancialTransaction
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly Date { get; set; }
    public Guid? AssociatedTransactionId { get; set; }
    public required FinancialTransactionStatus Status { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
    public FinancialTransaction? AssociatedTransaction { get; set; }
}
