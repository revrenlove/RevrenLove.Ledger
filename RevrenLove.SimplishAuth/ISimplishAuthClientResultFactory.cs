namespace RevrenLove.SimplishAuth;

internal interface ISimplishAuthClientResultFactory
{
    Task<SimplishAuthClientResult> CreateAsync(HttpResponseMessage httpResponse, bool isValidated = false);
    Task<SimplishAuthClientResult<T>> CreateAsync<T>(HttpResponseMessage httpResponse, bool isValidated = false);
}