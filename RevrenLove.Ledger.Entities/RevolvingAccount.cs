using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class RevolvingAccount
{
    public Guid Id { get; set; }

    public Guid FinancialAccountId { get; set; }

    public DateOnly StartDate { get; set; }
    public Frequency Frequency { get; set; }
    public decimal MinimumPaymentAmount { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
