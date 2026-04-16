using Riok.Mapperly.Abstractions;

namespace RevrenLove.Ledger.Api;

[Mapper]
public partial class Mapper
{
    public partial Models.FinancialAccount ToApiModel(Services.Models.FinancialAccount serviceModel);
    public partial Services.Models.FinancialAccount ToServiceModel(Models.FinancialAccount apiModel);

    public partial Models.RecurringTransaction ToApiModel(Services.Models.RecurringTransaction serviceModel);
    public partial Services.Models.RecurringTransaction ToServiceModel(Models.RecurringTransaction apiModel);


    [MapProperty(nameof(Services.Models.FinancialTransaction.RunningBalance), nameof(Models.FinancialTransaction.RunningBalance), Use = nameof(MapRunningBalance))]
    public partial Models.FinancialTransaction ToApiModel(Services.Models.FinancialTransaction serviceModel);
    public partial Services.Models.FinancialTransaction ToServiceModel(Models.FinancialTransaction apiModel);

    public Services.Models.FinancialTransaction ToServiceModel(Models.CreateFinancialTransactionRequest apiModel)
    {
        var financialTransaction = new Services.Models.FinancialTransaction()
        {
            FinancialAccountId = apiModel.FinancialAccountId,
            Amount = apiModel.Amount,
            Date = apiModel.Date,
            Description = apiModel.Description,
            Status = apiModel.Status,
        };

        return financialTransaction;
    }

    private static decimal MapRunningBalance(decimal? runningBalance)
    {
        return runningBalance ?? throw new InvalidOperationException("RunningBalance cannot be null when mapping to API model.");
    }
}
