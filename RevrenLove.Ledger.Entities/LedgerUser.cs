using Microsoft.AspNetCore.Identity;

namespace RevrenLove.Ledger.Entities;

public class LedgerUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
