using RevrenLove.Ledger.Models;

namespace RevrenLove.Ledger.Services;

public class FinancialAccountHolderService(IDataAccessor<Entities.FinancialAccountHolder> financialAccountHolders)
{
    public async Task<FinancialAccountHolder> AddAsync(string name, string? description = null)
    {
        var entity = new Entities.FinancialAccountHolder
        {
            Name = name,
            Description = description,
            IsActive = true,
        };

        await financialAccountHolders.AddAsync(entity);

        var model = FinancialAccountHolder.FromEntity(entity);

        return model;
    }

    public async Task<FinancialAccountHolder> GetAsync(Guid id)
    {
        var entity =
            await
                financialAccountHolders.GetAsync(id) ??
                    throw new KeyNotFoundException($"No {nameof(FinancialAccountHolder)} found with Id of {id}");

        var model = FinancialAccountHolder.FromEntity(entity);

        return model;
    }

    public async Task<FinancialAccountHolder> UpdateAsync(FinancialAccountHolder financialAccountHolder)
    {
        var entity = financialAccountHolder.ToEntity();

        entity = await financialAccountHolders.UpdateAsync(entity);

        var model = FinancialAccountHolder.FromEntity(entity);

        return model;
    }
}
