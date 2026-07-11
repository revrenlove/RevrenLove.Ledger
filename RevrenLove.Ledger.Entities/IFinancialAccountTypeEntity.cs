namespace RevrenLove.Ledger.Entities;

public interface IFinancialAccountTypeEntity : IEntity
{
    Guid FinancialAccountId { get; set; }
    public FinancialAccount? FinancialAccount { get; set; }
}
