using System.Net;
using System.Net.Http.Json;
using RevrenLove.SimplishAuth.Client.Models;

namespace RevrenLove.SimplishAuth.Client;

public class SimplishAuthClientResult<T> : SimplishAuthClientResult
{
    public T? Value { get; private set; }

    private SimplishAuthClientResult(HttpResponseMessage httpResponse)
        : base(httpResponse) { }

    public static new async Task<SimplishAuthClientResult<T>> CreateAsync(HttpResponseMessage httpResponse, bool isValidated = false)
    {
        var result = new SimplishAuthClientResult<T>(httpResponse);

        if (result.IsSuccessStatusCode)
        {
            result.Value = await httpResponse.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            await result.HandleValidation(isValidated);
        }

        return result;
    }
}

public class SimplishAuthClientResult
{
    public HttpStatusCode StatusCode => HttpResponse.StatusCode;
    public bool IsSuccessStatusCode => HttpResponse.IsSuccessStatusCode;
    public string? ReasonPhrase => HttpResponse.ReasonPhrase;
    public HttpValidationProblemDetails? HttpValidationProblemDetails { get; internal set; }

    protected HttpResponseMessage HttpResponse;

    protected SimplishAuthClientResult(HttpResponseMessage httpResponse) => HttpResponse = httpResponse;

    public static async Task<SimplishAuthClientResult> CreateAsync(HttpResponseMessage httpResponse, bool isValidated = false)
    {
        var result = new SimplishAuthClientResult(httpResponse);

        await result.HandleValidation(isValidated);

        return result;
    }

    protected async Task HandleValidation(bool isValidated)
    {
        if (!isValidated || StatusCode != HttpStatusCode.BadRequest)
        {
            return;
        }

        var httpValidationProblemDetails = await HttpResponse.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

        HttpValidationProblemDetails = httpValidationProblemDetails;
    }
}
