using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Api.Models;

public record FinancialTransaction : IModel
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly Date { get; set; }
    public Guid? AssociatedTransactionId { get; set; }
    public required FinancialTransactionStatus Status { get; set; }
    public required decimal RunningBalance { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
    public FinancialTransaction? AssociatedTransaction { get; set; }
}
