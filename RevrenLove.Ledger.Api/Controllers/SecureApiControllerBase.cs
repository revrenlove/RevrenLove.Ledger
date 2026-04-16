using Microsoft.AspNetCore.Authorization;

namespace RevrenLove.Ledger.Api.Controllers;

[Authorize]
public class SecureApiControllerBase() : ApiControllerBase
{
    
}
