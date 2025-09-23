using System.Net;

namespace RevrenLove.SimplishAuth;

public class SimplishAuthClientResult<TResponse, TError>(
    TResponse response,
    HttpResponseMessage httpResponse,
    TError error = default!)
{
    public TResponse Response { get; set; } = response;
    public HttpStatusCode StatusCode { get; set; } = httpResponse.StatusCode;
    public bool IsSuccessStatusCode { get; set; } = httpResponse.IsSuccessStatusCode;
    public string? ReasonPhrase { get; set; } = httpResponse.ReasonPhrase;
    public TError Error { get; set; } = error;
    public string? Content { get; set; } = httpResponse.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
}

// public class SimplishAuthClientResult<TError>(
//     HttpResponseMessage httpResponse,
//     TError error)
//         : SimplishAuthClientResult(httpResponse)
// {
//     public TError Error { get; set; } = error;
// }

// public class SimplishAuthClientResult<TResponse>(
//     TResponse response,
//     HttpResponseMessage httpResponse)
//         : SimplishAuthClientResult(httpResponse)
// {

// }

public class SimplishAuthClientResult(HttpResponseMessage httpResponse)
{
    public HttpStatusCode StatusCode { get; set; } = httpResponse.StatusCode;
    public bool IsSuccessStatusCode { get; set; } = httpResponse.IsSuccessStatusCode;
    public string? ReasonPhrase { get; set; } = httpResponse.ReasonPhrase;
    public string? Content { get; set; } = httpResponse.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
}