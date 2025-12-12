using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class FinancialAccountsController(
    IFinancialAccountsService financialAccountsService,
    UserManager<Entities.LedgerUser> userManager) : SecureApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<FinancialAccount>>> GetAsync(CancellationToken cancellationToken)
    {
        //var userId = GetUserId();

        //var domainAccounts = await financialAccountsService.GetByUserIdAsync(userId, cancellationToken);
        //var apiAccounts = domainAccounts.Select(x => x.ToApiModel()).ToList().AsReadOnly();

        //return Ok(apiAccounts);

        throw new NotImplementedException();
    }

    private Guid GetUserId() => Guid.Parse(userManager.GetUserId(User)!);
}
