using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public interface IRecurringTransactionAccount
{
    public DateOnly StartDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public Frequency Frequency { get; set; }
}
