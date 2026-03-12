using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RevrenLove.Ledger.Api.Client;

public class ApiClientResult<T> : ApiClientResult
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public T? Value { get; private set; }
    public string? RawContent { get; private set; }

    private ApiClientResult(HttpResponseMessage httpResponse)
        : base(httpResponse) { }

    public static new async Task<ApiClientResult<T>> CreateAsync(HttpResponseMessage httpResponse)
    {
        var result = new ApiClientResult<T>(httpResponse)
        {
            RawContent = await httpResponse.Content.ReadAsStringAsync()
        };

        if (!result.IsSuccessStatusCode)
        {
            return result;
        }

        result.Value = JsonSerializer.Deserialize<T>(result.RawContent, _jsonSerializerOptions);

        return result;
    }
}

// TODO: JE - Maybe this should be an interface or something and then we can have a separate class for errors or something? I don't know, just feels weird to have the error information on the same class as the success information...
public class ApiClientResult
{
    public HttpStatusCode StatusCode => HttpResponse.StatusCode;
    public bool IsSuccessStatusCode => HttpResponse.IsSuccessStatusCode;
    public string? ReasonPhrase => HttpResponse.ReasonPhrase;

    // TODO: JE - Parse the error or some shit...
    protected HttpResponseMessage HttpResponse { get; }

    protected ApiClientResult(HttpResponseMessage httpResponse) => HttpResponse = httpResponse;

    public static async Task<ApiClientResult> CreateAsync(HttpResponseMessage httpResponse) =>
        new(httpResponse);
}
