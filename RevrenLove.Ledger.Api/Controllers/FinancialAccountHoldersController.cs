using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Abstractions.Services;
using RevrenLove.Ledger.Models;

namespace RevrenLove.Ledger.Api.Controllers;

public class FinancialAccountHoldersController(IFinancialAccountHolderService financialAccountHolderService) : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<FinancialAccountHolder>> Get(Guid id) =>
        await financialAccountHolderService.GetAsync(id);

    [HttpPost]
    public async Task<ActionResult<FinancialAccountHolder>> Post(FinancialAccountHolder financialAccountHolder) =>
        await financialAccountHolderService.AddAsync(financialAccountHolder);
}
