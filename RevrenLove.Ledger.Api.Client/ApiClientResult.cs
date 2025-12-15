using System.Net;
using System.Net.Http.Json;
using RevrenLove.SimplishAuth.Client;

namespace RevrenLove.Ledger.Api.Client;

public class ApiClientResult<T> : ApiClientResult
{
    public T? Value { get; private set; }

    private ApiClientResult(HttpResponseMessage httpResponse)
        : base(httpResponse) { }

    public static new async Task<ApiClientResult<T>> CreateAsync(HttpResponseMessage httpResponse)
    {
        var result = new ApiClientResult<T>(httpResponse);

        if (result.IsSuccessStatusCode)
        {
            return result;
        }

        try
        {
            result.Value = await result.HttpResponse.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            // TODO: Do something better here...
            throw;
        }

        return result;
    }
}

public class ApiClientResult
{
    public HttpStatusCode StatusCode => HttpResponse.StatusCode;
    public bool IsSuccessStatusCode => HttpResponse.IsSuccessStatusCode;
    public string? ReasonPhrase => HttpResponse.ReasonPhrase;
    public HttpValidationProblemDetails? HttpValidationProblemDetails { get; internal set; }

    protected HttpResponseMessage HttpResponse { get; }

    protected ApiClientResult(HttpResponseMessage httpResponse) => HttpResponse = httpResponse;

    public static async Task<ApiClientResult> CreateAsync(HttpResponseMessage httpResponse) =>
        new(httpResponse);
}
