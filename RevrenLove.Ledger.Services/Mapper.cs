using Riok.Mapperly.Abstractions;

namespace RevrenLove.Ledger.Services;

[Mapper]
public partial class Mapper()
{
    [MapperIgnoreSource(nameof(Entities.FinancialAccount.User))]
    [MapperIgnoreSource(nameof(Entities.FinancialAccount.UserId))]
    [MapperIgnoreSource(nameof(Entities.FinancialAccount.FinancialTransactions))]
    [MapperIgnoreSource(nameof(Entities.FinancialAccount.RecurringIncomingTransactionsTransactions))]
    [MapperIgnoreSource(nameof(Entities.FinancialAccount.RecurringOutgoingTransactionsTransactions))]
    public partial Models.FinancialAccount ToModel(Entities.FinancialAccount entity);

    [MapperIgnoreTarget(nameof(Entities.FinancialAccount.User))]
    [MapperIgnoreTarget(nameof(Entities.FinancialAccount.UserId))]
    public partial Entities.FinancialAccount ToEntity(Models.FinancialAccount model);

    [MapperIgnoreSource(nameof(Entities.FinancialTransaction.CorrelationId))]
    [MapperIgnoreTarget(nameof(Models.FinancialTransaction.AssociatedTransaction))]
    [MapperIgnoreTarget(nameof(Models.FinancialTransaction.AssociatedTransactionId))]
    [MapProperty([nameof(Entities.FinancialTransaction.RunningBalance), nameof(Entities.FinancialTransaction.RunningBalance.Balance)], nameof(Models.FinancialTransaction.RunningBalance))]
    public partial Models.FinancialTransaction ToModel(Entities.FinancialTransaction entity);

    [MapperIgnoreTarget(nameof(Entities.FinancialTransaction.CorrelationId))]
    [MapperIgnoreTarget(nameof(Entities.FinancialTransaction.RunningBalance))]
    [MapperIgnoreSource(nameof(Models.FinancialTransaction.AssociatedTransaction))]
    [MapperIgnoreSource(nameof(Models.FinancialTransaction.AssociatedTransactionId))]
    [MapperIgnoreSource(nameof(Models.FinancialTransaction.RunningBalance))]
    public partial Entities.FinancialTransaction ToEntity(Models.FinancialTransaction model);

    public Models.FinancialTransaction ToModel(FinancialTransactionWithCorrelation financialTransactionWithCorrelation)
    {
        Models.FinancialTransaction? associatedTransaction = null;
        Guid? associatedTransactionId = null;

        if (financialTransactionWithCorrelation.CorrelatedTransaction != null)
        {
            associatedTransactionId = financialTransactionWithCorrelation.CorrelatedTransaction.Id;
            associatedTransaction = ToModel(financialTransactionWithCorrelation.CorrelatedTransaction);
        }

        var financialTransaction = ToModel(financialTransactionWithCorrelation.Transaction);

        financialTransaction.AssociatedTransactionId = associatedTransactionId;
        financialTransaction.AssociatedTransaction = associatedTransaction;

        return financialTransaction;
    }

    public partial Entities.RecurringTransaction ToEntity(Models.RecurringTransaction model);
    public partial Models.RecurringTransaction ToModel(Entities.RecurringTransaction entity);
}
