using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RevrenLove.Ledger.Api.Client;

internal static class Extensions
{
    /// <summary>
    /// Sends a GET request with the supplied bearer token to the specified Uri and
    /// returns the value that results from deserializing the response body as JSON
    /// in an asynchronous operation.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <param name="httpClient">The client used to send the request.</param>
    /// <param name="bearerToken">The bearer token to use for the request.</param>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive
    /// notice of cancellation
    /// </param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">
    /// The cancellation token was canceled. This exception is stored into the
    /// returned task.
    /// </exception>
    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient httpClient, string bearerToken, string? requestUri, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        using var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return responseBody;
    }
}
