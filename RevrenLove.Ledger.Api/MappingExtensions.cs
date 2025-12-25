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
            IsBalanceExempt = serviceModel.IsBalanceExempt,
            IsActive = serviceModel.IsActive,
        };

    public static Services.Models.FinancialAccount ToServiceModel(this Models.FinancialAccount apiModel) =>
        new()
        {
            Id = apiModel.Id,
            FriendlyId = apiModel.FriendlyId,
            Name = apiModel.Name,
            Description = apiModel.Description,
            IsBalanceExempt = apiModel.IsBalanceExempt,
            IsActive = apiModel.IsActive,
        };

    public static Models.LedgerTransaction ToApiModel(this Services.Models.LedgerTransaction serviceModel) =>
        new()
        {
            Id = serviceModel.Id,
            FinancialAccountId = serviceModel.FinancialAccountId,
            Amount = serviceModel.Amount,
            Description = serviceModel.Description,
            DatePosted = serviceModel.DatePosted,
            CorrelationId = serviceModel.CorrelationId
        };

    public static Services.Models.LedgerTransaction ToServiceModel(this Models.LedgerTransaction apiModel) =>   
        new()
        {
            Id = apiModel.Id,
            FinancialAccountId = apiModel.FinancialAccountId,
            Amount = apiModel.Amount,
            Description = apiModel.Description,
            DatePosted = apiModel.DatePosted,
            CorrelationId = apiModel.CorrelationId
        };
}
