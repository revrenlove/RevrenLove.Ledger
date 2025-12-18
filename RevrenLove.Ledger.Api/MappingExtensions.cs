namespace RevrenLove.Ledger.Api;

public static class MappingExtensions
{
    public static Models.FinancialAccount ToApiModel(this Services.Models.FinancialAccount serviceModel) =>
        new()
        {
            Id = serviceModel.Id,
            FriendlyId = serviceModel.FriendlyId,
            Name = serviceModel.Name,
            Description = serviceModel.Description,
            IsActive = serviceModel.IsActive,
        };

    public static Services.Models.FinancialAccount ToServiceModel(this Models.FinancialAccount apiModel) =>
        new()
        {
            Id = apiModel.Id,
            FriendlyId = apiModel.FriendlyId,
            Name = apiModel.Name,
            Description = apiModel.Description,
            IsActive = apiModel.IsActive,
        };
}
