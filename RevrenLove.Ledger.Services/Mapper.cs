namespace RevrenLove.Ledger.Services;

public static class Mapper
{
    public static Models.AccountTypes.DepositAccount Map(Entities.DepositAccount source)
    {
        var target = new Models.AccountTypes.DepositAccount
        {
            AccountNumber = source.AccountNumber,
            RoutingNumber = source.RoutingNumber,
        };

        ApplyFinancialAccountProperties(source, target);

        return target;
    }

    public static Models.AccountTypes.IncomeSourceAccount Map(Entities.IncomeSourceAccount source)
    {
        var target = new Models.AccountTypes.IncomeSourceAccount
        {
            PaymentAmount = source.PaymentAmount,
            Frequency = source.Frequency,
        };

        ApplyFinancialAccountProperties(source, target);

        return target;
    }

    public static Models.AccountTypes.InstallmentAccount Map(Entities.InstallmentAccount source)
    {
        var target = new Models.AccountTypes.InstallmentAccount
        {
            TotalAmount = source.TotalAmount,
            StartDate = source.StartDate,
            PaymentAmount = source.PaymentAmount,
            Frequency = source.Frequency,
        };

        ApplyFinancialAccountProperties(source, target);

        return target;
    }

    public static Models.AccountTypes.RecurringExpenseAccount Map(Entities.RecurringExpenseAccount source)
    {
        var target = new Models.AccountTypes.RecurringExpenseAccount
        {
            StartDate = source.StartDate,
            PaymentAmount = source.PaymentAmount,
            Frequency = source.Frequency,
            Category = source.Category,
        };

        ApplyFinancialAccountProperties(source, target);

        return target;
    }

    public static Models.AccountTypes.RevolvingAccount Map(Entities.RevolvingAccount source)
    {
        var target = new Models.AccountTypes.RevolvingAccount
        {
            StartDate = source.StartDate,
            PaymentAmount = source.PaymentAmount,
            Frequency = source.Frequency,
        };

        ApplyFinancialAccountProperties(source, target);

        return target;
    }

    private static void ApplyFinancialAccountProperties(Entities.IFinancialAccountTypeEntity source, Models.IFinancialAccount target)
    {
        if (source.FinancialAccount == null)
        {
            throw new InvalidOperationException($"Financial account null. Ensure the `FinancialAccount` is included in the query.");
        }

        target.Id = source.FinancialAccountId;
        target.MetadataId = source.Id;
        target.FriendlyId = source.FinancialAccount.FriendlyId;
        target.Name = source.FinancialAccount.Name;
        target.Description = source.FinancialAccount.Description;
    }
}   
