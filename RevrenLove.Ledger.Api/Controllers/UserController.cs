using Microsoft.AspNetCore.Mvc;

namespace RevrenLove.Ledger.Api.Controllers;

public class UserController() : SecureApiControllerBase
{
    [HttpGet]
    public ActionResult<UserInfoDto> Get()
    {
        var name = HttpContext.User.Identity?.Name;

        var claims = HttpContext.User.Claims.Select(c => new ClaimDto(c.Type, c.Value)).ToList();

        var response = new UserInfoDto(name, claims);

        return Ok(response);
    }
}

// TODO: JE - Maybe don't have these here....
public record UserInfoDto(string? Name, List<ClaimDto> Claims);

public record ClaimDto(string Type, string Value);