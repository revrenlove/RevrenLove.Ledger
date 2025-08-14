namespace RevrenLove.Ledger.Models;

public class FinancialAccount
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
