using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public record RecurringExpenseAccount : FinancialAccount
{
    public DateOnly StartDate { get; set; }
    public Frequency Frequency { get; set; }
    public decimal Amount { get; set; }
}
