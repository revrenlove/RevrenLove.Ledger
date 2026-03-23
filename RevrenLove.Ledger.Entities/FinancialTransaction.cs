using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class FinancialTransaction : IEntity
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public required string Description { get; set; }
    public required DateOnly Date { get; set; }
    public required FinancialTransactionStatus Status { get; set; }
    public Guid? CorrelationId { get; set; }
    public string ComputedDisplayValue { get; set; } = string.Empty;

    public FinancialAccount? FinancialAccount { get; set; }
    public RunningBalance? RunningBalance { get; set; }
}
