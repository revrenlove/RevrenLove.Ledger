using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class LedgerTransactionsController(
    ILedgerTransactionService ledgerTransactionService,
    Mapper mapper)
    : SecureApiControllerBase
{
    [HttpGet("{transactionId:guid}")]
    public async Task<ActionResult<LedgerTransaction>> GetAsync(Guid transactionId)
    {
        try
        {
            var transaction = await ledgerTransactionService.GetAsync(transactionId);

            return Ok(mapper.ToApiModel(transaction));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LedgerTransaction>>> GetByFinancialAccountIdAsync([FromQuery] Guid financialAccountId)
    {
        var transactions = await ledgerTransactionService.GetByFinancialAccountIdAsync(financialAccountId);
        return Ok(transactions.Select(mapper.ToApiModel));
    }

    [HttpPost]
    public async Task<ActionResult<LedgerTransaction>> CreateAsync(LedgerTransaction request)
    {
        var serviceModel = mapper.ToServiceModel(request);

        var createdTransaction = await ledgerTransactionService.CreateAsync(serviceModel);

        var uri = Url.Content($"~/api/LedgerTransactions/{createdTransaction.Id}")!;

        var apiModel = mapper.ToApiModel(createdTransaction);

        return Created(uri, apiModel);
    }
}
