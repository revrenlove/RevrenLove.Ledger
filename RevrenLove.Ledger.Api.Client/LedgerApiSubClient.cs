using System.Net.Http.Json;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Client;

public interface ILedgerApiSubClient<T>
    where T : IModel
{
    Task<ApiClientResult<T>> CreateAsync(T model, CancellationToken cancellationToken = default);
    Task<ApiClientResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiClientResult<List<T>>> GetAsync(CancellationToken cancellationToken = default);
    Task<ApiClientResult<T>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiClientResult<T>> UpdateAsync(T model, CancellationToken cancellationToken = default);
}

internal abstract class LedgerApiSubClient<T>(HttpClient httpClient) : ILedgerApiSubClient<T>
    where T : IModel
{
    public async Task<ApiClientResult<List<T>>> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(cancellationToken: cancellationToken);

        var result = await ApiClientResult<List<T>>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult<T>> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(id.ToString(), cancellationToken);

        var result = await ApiClientResult<T>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult<T>> CreateAsync(
        T model,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(model, cancellationToken);

        var result = await ApiClientResult<T>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult<T>> UpdateAsync(
        T model,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(model.Id.ToString(), model, cancellationToken);

        var result = await ApiClientResult<T>.CreateAsync(response);

        return result;
    }

    public async Task<ApiClientResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(id.ToString(), cancellationToken);

        var result = await ApiClientResult.CreateAsync(response);

        return result;
    }

    protected HttpClient HttpClient => httpClient;
}
