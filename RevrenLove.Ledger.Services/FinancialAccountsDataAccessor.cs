//using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence;
using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services;

public interface IFinancialAccountsDataAccessor : IDataAccessor<Entities.FinancialAccount>
{
}

internal class FinancialAccountsDataAccessor(
    IDataAccessor<Entities.DepositAccount> depositAccounts,
    IDataAccessor<Entities.IncomeSourceAccount> incomeSourceAccounts,
    IDataAccessor<Entities.InstallmentAccount> installmentAccounts,
    IDataAccessor<Entities.RecurringExpenseAccount> recurringExpenseAccounts,
    IDataAccessor<Entities.RevolvingAccount> revolvingAccounts,
    LedgerDbContext dbContext) 
        : DataAccessor<Entities.FinancialAccount>(dbContext),
        IFinancialAccountsDataAccessor
    // : Dictionary<FinancialAccountType, IDataAccessor<Entities.IFinancialAccountTypeEntity>>
{
    public IDataAccessor<Entities.DepositAccount> DepositAccounts => depositAccounts;
    public IDataAccessor<Entities.IncomeSourceAccount> IncomeSourceAccounts => incomeSourceAccounts;
    public IDataAccessor<Entities.InstallmentAccount> InstallmentAccounts => installmentAccounts;
    public IDataAccessor<Entities.RecurringExpenseAccount> RecurringExpenseAccounts => recurringExpenseAccounts;
    public IDataAccessor<Entities.RevolvingAccount> RevolvingAccounts => revolvingAccounts;

    //public async Task<Entities.IFinancialAccountTypeEntity> JimTest()
}
