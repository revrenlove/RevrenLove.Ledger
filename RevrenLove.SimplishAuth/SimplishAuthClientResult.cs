using System.Net;
using RevrenLove.SimplishAuth.Models;

namespace RevrenLove.SimplishAuth;

public class SimplishAuthClientResult<T>(HttpResponseMessage httpResponse) : SimplishAuthClientResult(httpResponse)
{
    public T? Value { get; internal set; }
}

public class SimplishAuthClientResult
{
    public HttpStatusCode StatusCode { get; }
    public bool IsSuccessStatusCode { get; }
    public string? ReasonPhrase { get; }
    public HttpValidationProblemDetails? HttpValidationProblemDetails { get; internal set; }

    internal SimplishAuthClientResult(HttpResponseMessage httpResponse)
    {
        StatusCode = httpResponse.StatusCode;
        IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
        ReasonPhrase = httpResponse.ReasonPhrase;
    }
}
