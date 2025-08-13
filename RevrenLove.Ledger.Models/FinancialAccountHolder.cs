namespace RevrenLove.Ledger.Models;

public class FinancialAccountHolder
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
