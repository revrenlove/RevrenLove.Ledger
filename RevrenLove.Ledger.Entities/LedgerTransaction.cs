namespace RevrenLove.Ledger.Entities;

public class LedgerTransaction : IEntity
{
    public Guid Id { get; set; }
    public required Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly DatePosted { get; set; }
    public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid CorrelationId { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
