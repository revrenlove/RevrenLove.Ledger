using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RevrenLove.Ledger.Api.Configuration;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Api.Auth;

public class JwtTokenService(
    JwtSecurityTokenHandler jwtSecurityTokenHandler,
    IOptions<AppSettings> options) : IJwtTokenService
{
    private readonly AppSettings _appSettings = options.Value;

    public string GenerateToken(LedgerUser user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Jwt.Key);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        var token = new JwtSecurityToken(
            issuer: _appSettings.Jwt.Issuer,
            audience: _appSettings.Jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_appSettings.Jwt.ExpirationInMinutes),
            signingCredentials: creds
        );

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
