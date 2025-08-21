namespace RevrenLove.Ledger.Entities;

public class FinancialAccountHolder : IEntity, IActivable
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required bool IsActive { get; set; } = true;

    public ICollection<FinancialAccount> FinancialAccounts { get; } = new HashSet<FinancialAccount>();
    public ICollection<LedgerItem> LedgerItems { get; } = new HashSet<LedgerItem>();
}
