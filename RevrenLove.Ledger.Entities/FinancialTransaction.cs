using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class FinancialTransaction : IEntity
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly Date { get; set; }
    public required FinancialTransactionStatus Status { get; set; }
    public Guid? CorrelationId { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
