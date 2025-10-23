using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence;

// TODO: JE - Figure out if we actually want to put an interface on this and make the context internal
public class LedgerDbContext(DbContextOptions<LedgerDbContext> options)
    : IdentityDbContext<LedgerUser, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
