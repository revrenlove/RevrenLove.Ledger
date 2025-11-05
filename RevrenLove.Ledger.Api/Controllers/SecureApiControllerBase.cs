using Microsoft.AspNetCore.Authorization;

namespace RevrenLove.Ledger.Api.Controllers;

[Authorize]
public abstract class SecureApiControllerBase : ApiControllerBase
{

}
