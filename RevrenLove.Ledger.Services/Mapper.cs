using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface IMapper
{
    FinancialAccount ToModel(Entities.FinancialAccount entity);
    Entities.FinancialAccount ToEntity(FinancialAccount entity);

    FinancialTransaction ToModel(Entities.FinancialTransaction entity);
    Entities.FinancialTransaction ToEntity(FinancialTransaction entity);

    RecurringTransaction ToModel(Entities.RecurringTransaction entity);
    Entities.RecurringTransaction ToEntity(RecurringTransaction entity);

    FinancialTransaction ToModel(FinancialTransactionWithCorrelation financialTransactionWithCorrelation);
}

internal class Mapper : IMapper
{
    public Entities.FinancialAccount ToEntity(FinancialAccount entity)
    {
        return new Entities.FinancialAccount
        {
            Id = entity.Id,
            FriendlyId = entity.FriendlyId,
            Name = entity.Name,
            Description = entity.Description,
            AccountType = entity switch
            {
                DepositAccount => Shared.FinancialAccountType.Deposit,
                InstallmentAccount => Shared.FinancialAccountType.Installment,
                RecurringExpenseAccount => Shared.FinancialAccountType.RecurringExpense,
                RevolvingAccount => Shared.FinancialAccountType.Revolving,
                _ => throw new ArgumentException($"Unknown financial account type: {entity.GetType().Name}", nameof(entity))
            }
        };
    }

    public Entities.FinancialTransaction ToEntity(FinancialTransaction entity)
    {
        throw new NotImplementedException();
    }

    public Entities.RecurringTransaction ToEntity(RecurringTransaction entity)
    {
        throw new NotImplementedException();
    }

    public FinancialAccount ToModel(Entities.FinancialAccount entity)
    {
        throw new NotImplementedException();
    }

    public FinancialTransaction ToModel(Entities.FinancialTransaction entity)
    {
        throw new NotImplementedException();
    }

    public FinancialTransaction ToModel(FinancialTransactionWithCorrelation financialTransactionWithCorrelation)
    {
        throw new NotImplementedException();
    }

    public RecurringTransaction ToModel(Entities.RecurringTransaction entity)
    {
        throw new NotImplementedException();
    }
}
