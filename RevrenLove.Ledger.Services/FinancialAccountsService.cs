using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence.SQLite;
using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface IFinancialAccountsService : ILedgerServiceBase<FinancialAccount, Entities.FinancialAccount>
{
    Task<FinancialAccount> GetAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
    Task<ICollection<FinancialAccount>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<FinancialAccount> CreateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken = default);
    Task<FinancialAccount> UpdateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

internal class FinancialAccountsService(LedgerSQLiteDbContext dbContext)
    : LedgerServiceBase<FinancialAccount, Entities.FinancialAccount>(dbContext), IFinancialAccountsService
{
    async Task<FinancialAccount> IFinancialAccountsService.GetAsync(Guid financialAccountId, CancellationToken cancellationToken) =>
        await GetAsync(financialAccountId, cancellationToken);

    async Task<ICollection<FinancialAccount>> IFinancialAccountsService.GetByUserAsync(Guid userId, CancellationToken cancellationToken) =>
        await GetAsync(fa => fa.Where(fa => fa.UserId == userId), cancellationToken);

    public async Task<FinancialAccount> CreateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken) =>
        await
            CreateAsync(
                financialAccount,
                entity =>
                {
                    entity.UserId = userId;
                },
                cancellationToken);

    public async Task<FinancialAccount> UpdateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken = default) =>
        await
            UpdateAsync(
                financialAccount,
                entity =>
                {
                    entity.UserId = userId;
                },
                cancellationToken);

    async Task IFinancialAccountsService.DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await DeleteAsync(id, cancellationToken);

    protected override FinancialAccount ToServiceModel(Entities.FinancialAccount entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description
    };

    protected override Entities.FinancialAccount ToEntity(FinancialAccount model, Action<Entities.FinancialAccount>? configureEntity = null)
    {
        var entity = new Entities.FinancialAccount()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IsActive = model.IsActive,
            UserId = default, // This will be set in the configureEntity action
        };

        configureEntity?.Invoke(entity);

        return entity;
    }
}
