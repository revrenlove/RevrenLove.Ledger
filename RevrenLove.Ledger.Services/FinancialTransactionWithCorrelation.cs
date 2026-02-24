namespace RevrenLove.Ledger.Services;

public class FinancialTransactionWithCorrelation(Entities.FinancialTransaction transaction, Entities.FinancialTransaction? correlatedTransaction)
{
    public Entities.FinancialTransaction Transaction => transaction;
    public Entities.FinancialTransaction? CorrelatedTransaction => correlatedTransaction;
}
