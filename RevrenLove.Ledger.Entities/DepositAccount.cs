namespace RevrenLove.Ledger.Entities;

public class DepositAccount : IEntity, IFinancialAccountTypeEntity
{
    public Guid Id { get; set; }

    public Guid FinancialAccountId { get; set; }

    public string? AccountNumber { get; set; }
    public string? RoutingNumber { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
