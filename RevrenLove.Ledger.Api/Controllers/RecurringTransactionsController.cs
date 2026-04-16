using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class RecurringTransactionsController(
    IRecurringTransactionService recurringTransactionService,
    Mapper mapper,
    UserManager<Entities.LedgerUser> userManager)
        : SecureApiControllerBase
{
    private readonly IRecurringTransactionService _recurringTransactionService = recurringTransactionService;
    private readonly Mapper _mapper = mapper;
    private readonly UserManager<Entities.LedgerUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.RecurringTransaction>>> GetAsync()
    {
        var userId = _userManager.GetUserIdAsGuid(User);
        
        var transactions = await _recurringTransactionService.GetByUserAsync(userId);
        
        var apiTransactions = transactions.Select(_mapper.ToApiModel);
        
        return Ok(apiTransactions);
    }
}
