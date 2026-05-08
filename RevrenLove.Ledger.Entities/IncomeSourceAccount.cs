using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class IncomeSourceAccount
{
    public Guid Id { get; set; }

    public Guid FinancialAccountId { get; set; }
    public decimal Amount { get; set; }
    public Frequency Frequency { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}