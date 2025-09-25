using System.Net;
using System.Net.Http.Json;
using RevrenLove.SimplishAuth.Models;

namespace RevrenLove.SimplishAuth;

internal class SimplishAuthClientResultFactory : ISimplishAuthClientResultFactory
{
    public async Task<SimplishAuthClientResult> CreateAsync(HttpResponseMessage httpResponse, bool isValidated = false)
    {
        var result = new SimplishAuthClientResult(httpResponse);

        if (isValidated && result.StatusCode == HttpStatusCode.BadRequest)
        {
            var httpValidationProblemDetails = await httpResponse.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

            result.HttpValidationProblemDetails = httpValidationProblemDetails;
        }

        return result;
    }

    public async Task<SimplishAuthClientResult<T>> CreateAsync<T>(HttpResponseMessage httpResponse, bool isValidated = false)
    {
        var result = new SimplishAuthClientResult<T>(httpResponse);

        if (result.IsSuccessStatusCode)
        {
            result.Value = await httpResponse.Content.ReadFromJsonAsync<T>();
        }
        else if (isValidated && result.StatusCode == HttpStatusCode.BadRequest)
        {
            var httpValidationProblemDetails = await httpResponse.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

            result.HttpValidationProblemDetails = httpValidationProblemDetails;
        }

        return result;
    }
}