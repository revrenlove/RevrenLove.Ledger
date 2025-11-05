namespace RevrenLove.Ledger.Api.Models;

public record JwtTokenResponse(string? Token, string TokenType, int ExpiresIn);
