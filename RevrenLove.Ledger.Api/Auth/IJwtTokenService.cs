using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Api.Auth;

public interface IJwtTokenService
{
    string GenerateToken(LedgerUser user);
}
