using RevrenLove.SimplishAuth.Client;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    /// <summary>
    /// Registers the SimplishAuthClient. This will use the `HttpClient` already registered in the DI container.
    /// </summary>
    /// <param name="services">The `Microsoft.Extensions.DependencyInjection.IServiceCollection` to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSimplishAuthClientAsService(this IServiceCollection services)
    {
        services
            .AddScoped<ISimplishAuthClient, SimplishAuthClient>();

        return services;
    }

    /// <summary>
    /// Registers the SimplishAuthClient that will use the supplied `HttpClient` instance.
    /// </summary>
    /// <param name="services">The `Microsoft.Extensions.DependencyInjection.IServiceCollection` to add the service to.</param>
    /// <param name="httpClient">The `HttpClient` that `SimplishAuthClient` will use.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSimplishAuthClientAsService(this IServiceCollection services, HttpClient httpClient)
    {
        services
            .AddScoped<ISimplishAuthClient, SimplishAuthClient>(_ => new(httpClient));

        return services;
    }

    /// <summary>
    /// Registers the SimplishAuthClient that will use the registered HttpClient with the supplied key.
    /// </summary>
    /// <param name="services">The `Microsoft.Extensions.DependencyInjection.IServiceCollection` to add the service to.</param>
    /// <param name="serviceKey">The key for the registered HttpClient</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSimplishAuthClientAsService(this IServiceCollection services, string serviceKey)
    {
        services.AddScoped<ISimplishAuthClient, SimplishAuthClient>(sp =>
        {
            var httpClient = sp.GetRequiredKeyedService<HttpClient>(serviceKey);

            return new(httpClient);
        });

        return services;
    }
}
