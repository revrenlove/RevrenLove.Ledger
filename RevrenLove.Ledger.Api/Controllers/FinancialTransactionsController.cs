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

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<FinancialTransaction>>> GetAsync(CancellationToken cancellationToken = default)
    //{
    //    var userId = GetUserId();

    //    var transactions = await _financialTransactionService.GetByUserAsync(userId, cancellationToken);

    //    var apiTransactions = transactions.Select(_mapper.ToApiModel);

    //    return Ok(apiTransactions);
    //}

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialTransaction>>> GetByFinancialAccountIdAsync([FromQuery] Guid financialAccountId, CancellationToken cancellationToken = default)
    {
        var transactions = await _financialTransactionService.GetByFinancialAccountIdAsync(financialAccountId, cancellationToken);

        var response = transactions.Select(_mapper.ToApiModel);

        return Ok(response);
    }

    // TODO: JE - Verify that the user has access to the financial account before creating the transaction
    [HttpPost]
    public async Task<ActionResult<FinancialTransaction>> CreateAsync(CreateFinancialTransactionRequest requestBody, CancellationToken cancellationToken = default)
    {
        var serviceModel = _mapper.ToServiceModel(requestBody);

        serviceModel = await _financialTransactionService.CreateAsync(serviceModel, requestBody.DestinationFinancialAccountId, cancellationToken);

        var apiModel = _mapper.ToApiModel(serviceModel);

        return Created(Url.Content($"~/api/FinancialAccounts/{apiModel.Id}"), apiModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FinancialTransaction>> UpdateAsync(Guid id, FinancialTransaction financialTransaction, CancellationToken cancellationToken = default)
    {
        if (id != financialTransaction.Id)
        {
            return BadRequest("Transaction ID in URL does not match transaction ID in request body.");
        }

        var serviceModel = _mapper.ToServiceModel(financialTransaction);

        try
        {
            serviceModel = await _financialTransactionService.UpdateAsync(serviceModel, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        financialTransaction = _mapper.ToApiModel(serviceModel);

        return Ok(financialTransaction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _financialTransactionService.DeleteAsync(id, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/post")]
    public async Task<IActionResult> PostTransactionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _financialTransactionService.PostAsync(id, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}