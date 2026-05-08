using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Entities;

public class InstallmentAccount
{
    public Guid Id { get; set; }
    
    public Guid FinancialAccountId { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateOnly StartDate { get; set; }
    public Frequency PaymentFrequency { get; set; }

    public FinancialAccount? FinancialAccount { get; set; }
}
