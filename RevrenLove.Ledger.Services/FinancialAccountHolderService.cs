using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Abstractions.Services;
using RevrenLove.Ledger.Models;
using RevrenLove.Ledger.Services.Extensions;

namespace RevrenLove.Ledger.Services;

internal class FinancialAccountHolderService(IDataAccessor<Entities.FinancialAccountHolder> financialAccountHolders) : IFinancialAccountHolderService
{
    public async Task<FinancialAccountHolder> AddAsync(FinancialAccountHolder financialAccountHolder)
    {
        var entity = financialAccountHolder.ToEntity();

        await financialAccountHolders.AddAsync(entity);

        var model = entity.ToModel();

        return model;
    }

    public async Task<FinancialAccountHolder> GetAsync(Guid id)
    {
        var entity =
            await
                financialAccountHolders.GetAsync(id) ??
                    throw new KeyNotFoundException($"No {nameof(FinancialAccountHolder)} found with Id of {id}");

        var model = entity.ToModel();

        return model;
    }

    public async Task<FinancialAccountHolder> UpdateAsync(FinancialAccountHolder financialAccountHolder)
    {
        var entity = financialAccountHolder.ToEntity();

        entity = await financialAccountHolders.UpdateAsync(entity);

        var model = entity.ToModel();

        return model;
    }
}
