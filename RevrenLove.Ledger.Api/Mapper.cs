using Riok.Mapperly.Abstractions;

namespace RevrenLove.Ledger.Api;

[Mapper]
public partial class Mapper
{
    public partial Models.FinancialAccount ToApiModel(Services.Models.FinancialAccount serviceModel);
    public partial Services.Models.FinancialAccount ToServiceModel(Models.FinancialAccount apiModel);
    public partial Models.LedgerTransaction ToApiModel(Services.Models.LedgerTransaction serviceModel);
    public partial Services.Models.LedgerTransaction ToServiceModel(Models.LedgerTransaction apiModel);
}
