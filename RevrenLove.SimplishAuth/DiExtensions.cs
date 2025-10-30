using RevrenLove.SimplishAuth;

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
    public static IServiceCollection AddSimplishAuthClient(this IServiceCollection services)
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
    public static IServiceCollection AddSimplishAuthClient(this IServiceCollection services, HttpClient httpClient)
    {
        services
            .AddScoped<ISimplishAuthClient>(_ => new SimplishAuthClient(httpClient));

        return services;
    }

    /// <summary>
    /// Registers the SimplishAuthClient that will use a new, isolated `HttpClient` instance with the specified `baseAddress`.
    /// </summary>
    /// <param name="services">The `Microsoft.Extensions.DependencyInjection.IServiceCollection` to add the service to.</param>
    /// <param name="baseAddress">The base address for the `HttpClient`.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSimplishAuthClient(this IServiceCollection services, string baseAddress)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };

        services
            .AddSimplishAuthClient(httpClient);

        return services;
    }
}
