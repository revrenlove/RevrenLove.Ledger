using System.ComponentModel.DataAnnotations;

namespace RevrenLove.Ledger.Api.Models;

public record FinancialAccount : IModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Friendly ID is required")]
    public string FriendlyId { get; set; } = null!;

    [Required(ErrorMessage = "Account name is required")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public bool IsBalanceExempt { get; set; } = false;

    public bool IsActive { get; set; }
}
