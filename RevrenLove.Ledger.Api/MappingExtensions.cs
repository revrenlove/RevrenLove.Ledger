namespace RevrenLove.Ledger.Api;

public static class MappingExtensions
{
    public static Models.FinancialAccount ToApiModel(this Services.Models.FinancialAccount serviceModel) =>
        new()
        {
            Id = serviceModel.Id,
            Name = serviceModel.Name,
            Description = serviceModel.Description,
        };

    public static Services.Models.FinancialAccount ToServiceModel(this Models.FinancialAccount apiModel) =>
        new()
        {
            Id = apiModel.Id,
            Name = apiModel.Name,
            Description = apiModel.Description,
        };
}
