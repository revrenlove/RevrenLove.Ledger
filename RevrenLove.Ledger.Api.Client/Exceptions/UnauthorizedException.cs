namespace RevrenLove.Ledger.Api.Client.Exceptions;

public class UnauthorizedException(HttpResponseMessage httpResponseMessage)
    : LedgerApiClientException(httpResponseMessage)
{

}
