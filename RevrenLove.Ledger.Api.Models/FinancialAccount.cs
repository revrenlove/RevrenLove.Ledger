namespace RevrenLove.Ledger.Api.Models;

public class FinancialAccount
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
