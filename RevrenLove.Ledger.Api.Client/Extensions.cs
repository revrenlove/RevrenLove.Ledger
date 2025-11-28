using System.Net.Http.Json;

namespace RevrenLove.Ledger.Api.Client;

internal static class Extensions
{
    /// <summary>
    /// Sends a GET request to the specified Uri and returns the value that results from
    /// deserializing the response body as JSON in an asynchronous operation.
    /// </summary>
    /// <param name="httpClient">The client used to send the request.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive
    /// notice of cancellation.
    /// </param>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">
    /// The cancellation token was canceled. This exception is stored into the returned
    /// task.
    /// </exception>
    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient httpClient, CancellationToken cancellationToken = default) =>
        await httpClient.GetFromJsonAsync<T>(string.Empty, cancellationToken);
}
