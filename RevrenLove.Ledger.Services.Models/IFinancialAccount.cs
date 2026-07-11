using RevrenLove.Ledger.Shared;

namespace RevrenLove.Ledger.Services.Models;

public interface IFinancialAccount
{
    Guid Id { get; set; }
    Guid MetadataId { get; set; }
    //FinancialAccountType FinancialAccountType { get; }
    string FriendlyId { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
}
