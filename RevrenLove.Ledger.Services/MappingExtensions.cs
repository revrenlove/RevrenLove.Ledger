namespace RevrenLove.Ledger.Services;

internal static class MappingExtensions
{
    public static Models.FinancialAccount ToServiceModel(this Entities.FinancialAccount entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
}
