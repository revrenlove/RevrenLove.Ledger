using Microsoft.AspNetCore.Identity;

namespace RevrenLove.Ledger.Entities;

public class LedgerUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
