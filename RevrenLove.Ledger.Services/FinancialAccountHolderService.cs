using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Abstractions.Services;
using RevrenLove.Ledger.Models;

namespace RevrenLove.Ledger.Services;

internal class FinancialAccountHolderService(
    IDataAccessor<Entities.FinancialAccountHolder> dataAccessor,
    IMapper mapper)
        : BaseService<FinancialAccountHolder, Entities.FinancialAccountHolder>(dataAccessor, mapper), IFinancialAccountHolderService
{
    // TODO: JE - the rest of the shit for this...
}
