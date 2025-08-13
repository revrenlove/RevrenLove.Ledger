namespace RevrenLove.Ledger.Services.Extensions;

public static class MappingExtensions
{
    public static Entities.FinancialAccountHolder ToEntity(this Models.FinancialAccountHolder model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IsActive = true,
        };

    public static Models.FinancialAccountHolder ToModel(this Entities.FinancialAccountHolder model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
}
