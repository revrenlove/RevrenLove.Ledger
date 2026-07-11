using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models.AccountTypes;

public record InstallmentAccount : IFinancialAccount, IRecurringTransactionAccount
{
    public Guid Id { get; set; }
    public Guid MetadataId { get; set; }
    public string FriendlyId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public decimal TotalAmount { get; set; }
    public DateOnly StartDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public Frequency Frequency { get; set; }
}
