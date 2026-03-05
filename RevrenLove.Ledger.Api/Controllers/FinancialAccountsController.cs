using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class FinancialAccountsController(
    IFinancialAccountsService financialAccountsService,
    Mapper mapper,
    UserManager<Entities.LedgerUser> userManager)
        : SecureApiControllerBase(userManager)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialAccount>>> GetAsync()
    {
        var userId = GetUserId();

        var accounts = await financialAccountsService.GetByUserAsync(userId);

        var apiAccounts = accounts.Select(mapper.ToApiModel);

        return Ok(apiAccounts);
    }

    [HttpGet("{accountId:guid}")]
    public async Task<ActionResult<FinancialAccount>> GetByIdAsync(Guid accountId)
    {
        try
        {
            var account = await financialAccountsService.GetAsync(accountId);

            return Ok(mapper.ToApiModel(account));
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

        var createdAccount = await financialAccountsService.CreateAsync(userId, mapper.ToServiceModel(request));
        
        return Created(Url.Content($"~/api/FinancialAccounts/{createdAccount.Id}"), mapper.ToApiModel(createdAccount));
    }

    [HttpPut("{accountId:guid}")]
    public async Task<ActionResult<FinancialAccount>> UpdateAsync(Guid accountId, FinancialAccount request)
    {
        if (accountId != request.Id)
        {
            return BadRequest("Account ID in URL does not match account ID in request body.");
        }

        var userId = GetUserId();

        try
        {
            var updatedAccount = await financialAccountsService.UpdateAsync(userId, mapper.ToServiceModel(request));

            return Ok(mapper.ToApiModel(updatedAccount));
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
}
