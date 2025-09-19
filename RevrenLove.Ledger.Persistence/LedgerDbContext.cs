using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence;

public class LedgerDbContext(DbContextOptions<LedgerDbContext> options)
    : IdentityDbContext<LedgerUser, IdentityRole<Guid>, Guid>(options)
{

}
