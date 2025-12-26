using RevrenLove.Ledger.Persistence.SQLite;
using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface ILedgerTransactionService
{
    Task<LedgerTransaction> CreateAsync(LedgerTransaction ledgerTransaction, CancellationToken cancellationToken = default);
    Task<LedgerTransaction> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<LedgerTransaction>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
}

internal class LedgerTransactionService(LedgerSQLiteDbContext dbContext)
    : LedgerServiceBase<LedgerTransaction, Entities.LedgerTransaction>(dbContext), ILedgerTransactionService
{
    public async Task<ICollection<LedgerTransaction>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken) =>
        await
            GetAsync(
                lt => lt.Where(lt => lt.FinancialAccountId == financialAccountId),
                cancellationToken);

    public async Task<LedgerTransaction> CreateAsync(LedgerTransaction ledgerTransaction, CancellationToken cancellationToken) =>
        await
            CreateAsync(
                ledgerTransaction,
                entity =>
                {
                    entity.CreatedOn = DateTime.UtcNow;
                },
                cancellationToken);

    async Task<LedgerTransaction> ILedgerTransactionService.GetAsync(Guid id, CancellationToken cancellationToken) =>
        await
            GetAsync(id, cancellationToken);

    protected override Entities.LedgerTransaction ToEntity(LedgerTransaction model, Action<Entities.LedgerTransaction>? configureEntity = null)
    {
        var entity = new Entities.LedgerTransaction
        {
            Id = model.Id,
            FinancialAccountId = model.FinancialAccountId,
            Amount = model.Amount,
            Description = model.Description,
            DatePosted = model.DatePosted,
            CorrelationId = model.CorrelationId,

            // This will be set in the configureEntity action
            CreatedOn = default,
        };

        configureEntity?.Invoke(entity);

        return entity;
    }

    protected override LedgerTransaction ToServiceModel(Entities.LedgerTransaction entity) =>
        new()
        {
            Id = entity.Id,
            FinancialAccountId = entity.FinancialAccountId,
            Amount = entity.Amount,
            Description = entity.Description,
            DatePosted = entity.DatePosted,
            CorrelationId = entity.CorrelationId
        };
}
