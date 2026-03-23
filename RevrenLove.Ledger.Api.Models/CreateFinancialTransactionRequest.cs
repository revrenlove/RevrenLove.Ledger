using System.ComponentModel.DataAnnotations;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Api.Models;

public class CreateFinancialTransactionRequest
{
    public Guid FinancialAccountId { get; set; }

    public Guid? DestinationFinancialAccountId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public required decimal Amount { get; set; }

    [StringLength(500, MinimumLength = 1, ErrorMessage = "Description is required and cannot exceed 500 characters")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public required DateOnly Date { get; set; }

    public required FinancialTransactionStatus Status { get; set; } = FinancialTransactionStatus.Posted;
}
