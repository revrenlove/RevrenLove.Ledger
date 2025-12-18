using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Persistence.SQLite;
using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface IFinancialAccountsService
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

    public async Task<FinancialAccount> CreateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken)
    {
        await ValidateFriendlyId(financialAccount.FriendlyId, userId);

        return
            await
                CreateAsync(
                    financialAccount,
                    entity =>
                    {
                        entity.UserId = userId;
                        entity.IsActive = true;
                    },
                    cancellationToken);
    }

    public async Task<FinancialAccount> UpdateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken = default)
    {
        await ValidateFriendlyId(financialAccount.FriendlyId, userId);

        return await
            UpdateAsync(
                financialAccount,
                entity =>
                {
                    entity.UserId = userId;
                },
                cancellationToken);
    }

    async Task IFinancialAccountsService.DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await DeleteAsync(id, cancellationToken);

    protected override FinancialAccount ToServiceModel(Entities.FinancialAccount entity) => new()
    {
        Id = entity.Id,
        FriendlyId = entity.FriendlyId,
        Name = entity.Name,
        Description = entity.Description,
        IsActive = entity.IsActive,
    };

    protected override Entities.FinancialAccount ToEntity(FinancialAccount model, Action<Entities.FinancialAccount>? configureEntity = null)
    {
        var entity = new Entities.FinancialAccount()
        {
            Id = model.Id,
            Name = model.Name,
            FriendlyId = model.FriendlyId,
            Description = model.Description,
            IsActive = model.IsActive,
            UserId = default, // This will be set in the configureEntity action
        };

        configureEntity?.Invoke(entity);

        return entity;
    }

    private async Task<bool> ValidateFriendlyId(string friendlyId, Guid userId)
    {
        if (await dbContext.FinancialAccounts.AnyAsync(fa => fa.FriendlyId == friendlyId && fa.UserId == userId))
        {
            var msg = $"A financial account with the friendly ID '{friendlyId}' already exists for the specified user.";

            throw new UniqueConstraintException(msg);
        }

        return await
            dbContext
                .FinancialAccounts
                .AnyAsync(fa => fa.FriendlyId == friendlyId && fa.UserId == userId);
    }
}
