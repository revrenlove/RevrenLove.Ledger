using System.ComponentModel.DataAnnotations;

namespace RevrenLove.Ledger.Api.Models;

public record LedgerTransaction : IModel
{
    public Guid Id { get; set; }
    public Guid FinancialAccountId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public required decimal Amount { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Date posted is required")]
    public required DateOnly DatePosted { get; set; }

    public Guid CorrelationId { get; set; }
}
