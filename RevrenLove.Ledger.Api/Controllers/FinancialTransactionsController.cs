using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class FinancialTransactionsController(
    IFinancialTransactionService financialTransactionService,
    Mapper mapper,
    UserManager<Entities.LedgerUser> userManager)
        : SecureApiControllerBase(userManager)
{
    private readonly IFinancialTransactionService _financialTransactionService = financialTransactionService;
    private readonly Mapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialTransaction>>> GetAsync(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();

        var transactions = await _financialTransactionService.GetByUserAsync(userId, cancellationToken);

        var apiTransactions = transactions.Select(_mapper.ToApiModel);

        return Ok(apiTransactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FinancialTransaction>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await _financialTransactionService.GetAsync(id, cancellationToken);

        if (transaction == null)
        {
            return NotFound();
        }

        var apiTransaction = _mapper.ToApiModel(transaction);

        return Ok(apiTransaction);
    }

    // TODO: JE - Verify that the user has access to the financial account before creating the transaction
    [HttpPost]
    public async Task<ActionResult<FinancialTransaction>> CreateAsync(FinancialTransaction model, CancellationToken cancellationToken = default)
    {
        var serviceModel = _mapper.ToServiceModel(model);

        var x = await _financialTransactionService.CreateAsync(serviceModel, cancellationToken);


        //return CreatedAtAction(nameof(GetByIdAsync), new { id = apiTransaction.Id }, apiTransaction);
    }
}