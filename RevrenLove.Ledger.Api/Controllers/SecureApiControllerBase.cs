using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace RevrenLove.Ledger.Api.Controllers;

[Authorize]
public class SecureApiControllerBase(UserManager<Entities.LedgerUser> userManager) : ApiControllerBase
{
    protected readonly UserManager<Entities.LedgerUser> UserManager = userManager;

    protected Guid GetUserId() => Guid.Parse(UserManager.GetUserId(User)!);
}
