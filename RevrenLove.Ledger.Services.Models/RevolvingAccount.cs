using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public record RevolvingAccount : FinancialAccount
{
    public DateOnly StartDate { get; set; }
    public Frequency Frequency { get; set; }
    public decimal MinimumPaymentAmount { get; set; }
}
