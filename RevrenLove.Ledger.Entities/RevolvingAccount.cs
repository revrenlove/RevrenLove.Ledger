using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class RevolvingAccount : IEntity, IFinancialAccountTypeEntity
{
    public Guid Id { get; set; }

    public Guid FinancialAccountId { get; set; }

    public DateOnly StartDate { get; set; }
    public Frequency Frequency { get; set; }
    public decimal PaymentAmount { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
