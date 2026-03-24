namespace RevrenLove.Ledger.Entities;

public class RunningBalance : IEntity
{
    public Guid Id { get; set; }
    public required Guid FinancialTransactionId { get; set; }
    public required decimal Balance { get; set; }

    public FinancialTransaction? FinancialTransaction { get; set; }
}
