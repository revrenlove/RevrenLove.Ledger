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
    public async Task<ActionResult<IEnumerable<FinancialAccount>>> GetAsync()
    {
        var userId = GetUserId();

        var accounts = await financialAccountsService.GetByUserAsync(userId);

        var apiAccounts = accounts.Select(x => x.ToApiModel());

        return Ok(apiAccounts);
    }

    [HttpGet("{accountId:guid}")]
    public async Task<ActionResult<FinancialAccount>> GetByIdAsync(Guid accountId)
    {
        try
        {
            var account = await financialAccountsService.GetAsync(accountId);

            return Ok(account.ToApiModel());
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<FinancialAccount>> CreateAsync(FinancialAccount request)
    {
        var userId = GetUserId();

        var createdAccount = await financialAccountsService.CreateAsync(userId, request.ToServiceModel());
        
        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { accountId = createdAccount.Id },
            createdAccount.ToApiModel());
    }

    [HttpPut("{accountId:guid}")]
    public async Task<ActionResult<FinancialAccount>> UpdateAsync(Guid accountId, FinancialAccount request)
    {
        try
        {
            var updatedAccount = await financialAccountsService.UpdateAsync(accountId, request.ToServiceModel());

            return Ok(updatedAccount.ToApiModel());
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{accountId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid accountId)
    {
        try
        {
            await financialAccountsService.DeleteAsync(accountId);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    private Guid GetUserId() => Guid.Parse(userManager.GetUserId(User)!);
}
