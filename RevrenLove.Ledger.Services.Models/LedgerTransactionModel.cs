namespace RevrenLove.Ledger.Services.Models;

public record LedgerTransactionModel
{
    public Guid Id { get; set; }
    public Guid FinancialAccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public required DateOnly DatePosted { get; set; }
    public Guid? CorrelationId { get; set; }
}
