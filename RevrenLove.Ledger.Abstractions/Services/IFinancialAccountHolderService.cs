using RevrenLove.Ledger.Models;

namespace RevrenLove.Ledger.Abstractions.Services;

// TODO: JE - Create a base service...
public interface IFinancialAccountHolderService
{
    Task<FinancialAccountHolder> AddAsync(FinancialAccountHolder financialAccountHolder);
    Task<FinancialAccountHolder> GetAsync(Guid id);
    Task<FinancialAccountHolder> UpdateAsync(FinancialAccountHolder financialAccountHolder);
}
