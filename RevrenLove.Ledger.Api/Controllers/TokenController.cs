// TODO: JE - Really go over this... It could be cleaner or something...

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Auth;
using RevrenLove.Ledger.Api.Models;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Api.Controllers;

public class TokenController(
    UserManager<LedgerUser> userManager,
    IJwtTokenService jwtTokenService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<JwtTokenResponse>> Post(EmailPasswordDto emailPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(emailPasswordDto.Email);
        if (user == null)
        {
            return Unauthorized();
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, emailPasswordDto.Password);
        if (!passwordValid)
        {
            return Unauthorized();
        }

        var token = jwtTokenService.GenerateToken(user);

        var response = new JwtTokenResponse(token, "bearer", 1800);

        return Ok(response);
    }
}

// TODO: JE - Maybe don't have these here....
public record LoginDto(string Email, string Password);

// public record JwtTokenResponse(string? Token, string TokenType, int ExpiresIn);
