namespace RevrenLove.Ledger.Models;

public class FinancialAccountHolder
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Entities.FinancialAccountHolder ToEntity() =>
        new()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            IsActive = true,
        };

    public static Entities.FinancialAccountHolder ToEntity(FinancialAccountHolder model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IsActive = true,
        };

    public static FinancialAccountHolder FromEntity(Entities.FinancialAccountHolder entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
}
