using System.ComponentModel.DataAnnotations;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Api.Models;

public class CreateFinancialTransactionRequest
{
    public Guid FinancialAccountId { get; set; }

    public Guid? DestinationFinancialAccountId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public required decimal Amount { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public required DateOnly Date { get; set; }

    public required FinancialTransactionStatus Status { get; set; } = FinancialTransactionStatus.Posted;
}
