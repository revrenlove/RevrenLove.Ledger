using RevrenLove.SimplishAuth;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddSimplishAuthClient(this IServiceCollection services)
    {
        services
            .AddScoped<ISimplishAuthClient, SimplishAuthClient>()
            .AddSingleton<ISimplishAuthClientResultFactory, SimplishAuthClientResultFactory>();

        return services;
    }
}
