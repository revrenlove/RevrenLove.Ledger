using Microsoft.EntityFrameworkCore;
//using RevrenLove.Ledger.Entities;
//using RevrenLove.Ledger.Persistence;
using RevrenLove.Ledger.Services.Models;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services;

public interface IFinancialAccountsService
{
    Task<IFinancialAccount> GetAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
    Task<ICollection<IFinancialAccount>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IFinancialAccount> CreateAsync(Guid userId, IFinancialAccount financialAccount, CancellationToken cancellationToken = default);
    Task<IFinancialAccount> UpdateAsync(Guid userId, IFinancialAccount financialAccount, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<decimal> GetPostedBalanceAsync(Guid financialAccountId, CancellationToken cancellationToken = default);
}

internal class FinancialAccountsService(
    IDataAccessor<Entities.FinancialAccount> financialAccounts,
    IDataAccessor<Entities.DepositAccount> depositAccounts,
    IDataAccessor<Entities.IncomeSourceAccount> incomeSourceAccounts,
    IDataAccessor<Entities.InstallmentAccount> installmentAccounts,
    IDataAccessor<Entities.RecurringExpenseAccount> recurringExpenseAccounts,
    IDataAccessor<Entities.RevolvingAccount> revolvingAccounts)
    //Mapper mapper)
    //: LedgerServiceBase<FinancialAccount, Entities.FinancialAccount>(dbContext), IFinancialAccountsService
    : IFinancialAccountsService
{
    private readonly IDataAccessor<Entities.FinancialAccount> _financialAccounts = financialAccounts;

    private readonly IDataAccessor<Entities.DepositAccount> _depositAccounts = depositAccounts;
    private readonly IDataAccessor<Entities.IncomeSourceAccount> _incomeSourceAccounts = incomeSourceAccounts;
    private readonly IDataAccessor<Entities.InstallmentAccount> _installmentAccounts = installmentAccounts;
    private readonly IDataAccessor<Entities.RecurringExpenseAccount> _recurringExpenseAccounts = recurringExpenseAccounts;
    private readonly IDataAccessor<Entities.RevolvingAccount> _revolvingAccounts = revolvingAccounts;

    public async Task<IFinancialAccount> GetAsync(Guid financialAccountId, CancellationToken cancellationToken)
    {
        FinancialAccountType accountType;

        try
        {
            accountType =
                await
                    _financialAccounts
                        .Where(fa => fa.Id == financialAccountId)
                        .Select(fa => fa.AccountType)
                        .FirstAsync(cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException($"Financial account with ID '{financialAccountId}' was not found.", ex);
        }

        var model = await GetModel(financialAccountId, accountType, cancellationToken);

        return model;
    }


    public void DeleteMe()
    {
        //IDataAccessor<Entities.FinancialAccount> s = new FinancialAccountsDataAccessor(null);
    }

    public async Task<ICollection<IFinancialAccount>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var accountTypesById = await _financialAccounts
            .Where(fa => fa.UserId == userId)
            .ToDictionaryAsync(fa => fa.Id, fa => fa.AccountType, cancellationToken);
    }

    private async Task<IFinancialAccount> GetModel(
        Guid financialAccountId,
        FinancialAccountType financialAccountType,
        CancellationToken cancellationToken = default)
    {
        Entities.IFinancialAccountTypeEntity entity;
        IFinancialAccount model;

        switch (financialAccountType)
        {
            case FinancialAccountType.Deposit:
                entity = await _depositAccounts.GetFinancialAccountTypeEntity(financialAccountId, cancellationToken);
                model = Mapper.Map((Entities.DepositAccount)entity);
                break;

            case FinancialAccountType.IncomeSource:
                entity = await _incomeSourceAccounts.GetFinancialAccountTypeEntity(financialAccountId, cancellationToken);
                model = Mapper.Map((Entities.IncomeSourceAccount)entity);
                break;

            case FinancialAccountType.Installment:
                entity = await _installmentAccounts.GetFinancialAccountTypeEntity(financialAccountId, cancellationToken);
                model = Mapper.Map((Entities.InstallmentAccount)entity);
                break;

            case FinancialAccountType.RecurringExpense:
                entity = await _recurringExpenseAccounts.GetFinancialAccountTypeEntity(financialAccountId, cancellationToken);
                model = Mapper.Map((Entities.RecurringExpenseAccount)entity);
                break;

            case FinancialAccountType.Revolving:
                entity = await _revolvingAccounts.GetFinancialAccountTypeEntity(financialAccountId, cancellationToken);
                model = Mapper.Map((Entities.RevolvingAccount)entity);
                break;

            default:
                throw new InvalidOperationException($"Unsupported financial account type: {financialAccountType}");
        }

        return model;
    }




    //private readonly Mapper _mapper = mapper;

    //async Task<FinancialAccount> IFinancialAccountsService.GetAsync(Guid financialAccountId, CancellationToken cancellationToken) =>
    //    await GetAsync(financialAccountId, cancellationToken);

    //public async Task<ICollection<FinancialAccount>> GetByUserAsync(Guid userId, CancellationToken cancellationToken) =>
    //    await GetAsync(fa => fa.Where(fa => fa.UserId == userId), cancellationToken);

    //public async Task<FinancialAccount> CreateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken)
    //{
    //    await ValidateFriendlyId(financialAccount.FriendlyId, userId);

    //    return
    //        await
    //            CreateAsync(
    //                financialAccount,
    //                entity =>
    //                {
    //                    entity.UserId = userId;
    //                },
    //                cancellationToken);
    //}

    //public async Task<FinancialAccount> UpdateAsync(Guid userId, FinancialAccount financialAccount, CancellationToken cancellationToken = default)
    //{
    //    await ValidateFriendlyId(financialAccount.FriendlyId, userId, financialAccount.Id);

    //    return await
    //        UpdateAsync(
    //            financialAccount,
    //            entity =>
    //            {
    //                entity.UserId = userId;
    //            },
    //            cancellationToken);
    //}

    //async Task IFinancialAccountsService.DeleteAsync(Guid id, CancellationToken cancellationToken) =>
    //    await DeleteAsync(id, cancellationToken);

    //public async Task<decimal> GetPostedBalanceAsync(Guid financialAccountId, CancellationToken cancellationToken = default) =>
    //    await
    //        DbContext
    //            .FinancialTransactions
    //            .Where(lt => lt.FinancialAccountId == financialAccountId && lt.Status == FinancialTransactionStatus.Posted)
    //            .SumAsync(lt => lt.Amount, cancellationToken);

    //protected override FinancialAccount ToServiceModel(Entities.FinancialAccount entity) => _mapper.ToModel(entity);

    //protected override Entities.FinancialAccount ToEntity(FinancialAccount model, Action<Entities.FinancialAccount>? configureEntity = null)
    //{
    //    var entity = _mapper.ToEntity(model);

    //    configureEntity?.Invoke(entity);

    //    return entity;
    //}

    private async Task ValidateFriendlyId(string friendlyId, Guid userId, Guid? excludeId = null)
    {
        //var query = DbContext.FinancialAccounts.Where(fa => fa.FriendlyId == friendlyId && fa.UserId == userId);
        var query = _financialAccounts.Where(fa => fa.FriendlyId == friendlyId && fa.UserId == userId);

        if (excludeId.HasValue)
        {
            query = query.Where(fa => fa.Id != excludeId.Value);
        }

        if (await query.AnyAsync(fa => fa.FriendlyId == friendlyId && fa.UserId == userId && fa.Id != excludeId))
        {
            var msg = $"A financial account with the friendly ID '{friendlyId}' already exists for the specified user.";

            throw new UniqueConstraintException(msg);
        }
    }
}
