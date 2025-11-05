namespace RevrenLove.Ledger.Api.Client.Exceptions;

public abstract class LedgerApiClientException : Exception
{
    public LedgerApiClientException(HttpResponseMessage httpResponseMessage)
    {
        HttpResponseMessage = httpResponseMessage;
    }

    public LedgerApiClientException(HttpResponseMessage httpResponseMessage, string message)
        : base(message)
    {
        HttpResponseMessage = httpResponseMessage;
    }

    public LedgerApiClientException(HttpResponseMessage httpResponseMessage, string message, Exception innerException)
        : base($"{message.EnsureTrailingPeriod()} See InnerException for details.", innerException)
    {
        HttpResponseMessage = httpResponseMessage;
    }

    public HttpResponseMessage HttpResponseMessage { get; }
}

file static class Extensions
{
    public static string EnsureTrailingPeriod(this string s)
    {
        if (s[^1] != '.')
        {
            s += ".";
        }

        return s;
    }
}
