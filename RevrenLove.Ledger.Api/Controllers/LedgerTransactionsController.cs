using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Services;

namespace RevrenLove.Ledger.Api.Controllers;

public class LedgerTransactionsController(ILedgerTransactionService ledgerTransactionService)
    : SecureApiControllerBase
{
    [HttpGet("{transactionId:guid}")]
    public async Task<ActionResult<LedgerTransaction>> GetAsync(Guid transactionId)
    {
        try
        {
            var transaction = await ledgerTransactionService.GetAsync(transactionId);

            return Ok(transaction.ToApiModel());
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
        return Ok(transactions.Select(t => t.ToApiModel()));
    }

    [HttpPost]
    public async Task<ActionResult<LedgerTransaction>> CreateAsync(LedgerTransaction request)
    {
        var serviceModel = request.ToServiceModel();

        var createdTransaction = await ledgerTransactionService.CreateAsync(serviceModel);

        var uri = Url.Content($"~/api/LedgerTransactions/{createdTransaction.Id}")!;

        var apiModel = createdTransaction.ToApiModel();

        return Created(uri, apiModel);
    }
}
