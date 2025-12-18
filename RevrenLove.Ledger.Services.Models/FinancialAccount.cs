namespace RevrenLove.Ledger.Services.Models;

public class FinancialAccount
{
    public Guid Id { get; set; }
    public required string FriendlyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsBalanceExempt { get; set; } = false;
    public bool IsActive { get; set; }
}
