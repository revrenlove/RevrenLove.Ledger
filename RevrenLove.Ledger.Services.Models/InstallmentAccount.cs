using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public record InstallmentAccount : FinancialAccount
{
    public decimal TotalAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateOnly StartDate { get; set; }
    public Frequency PaymentFrequency { get; set; }
}
