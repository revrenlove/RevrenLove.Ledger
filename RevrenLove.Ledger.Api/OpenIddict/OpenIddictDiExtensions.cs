#pragma warning disable IDE0130
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class OpenIddictDiExtensions
{
    public static IServiceCollection AddOpenIddict<TContext>(this IServiceCollection services, params string[] scopes)
        where TContext : DbContext
    {
        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options
                    .UseEntityFrameworkCore()
                    .UseDbContext<TContext>();
            })
            .AddServer(options =>
            {
                options
                    .SetTokenEndpointUris("/connect/token")
                    .SetAuthorizationEndpointUris("/connect/authorize");

                options
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow();

                options.RegisterScopes(scopes);

                options
                    .UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough();

                options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return services;
    }

    public static async Task<IApplicationBuilder> SeedOpenIddictClient(
        this WebApplication app,
        string clientId,
        params string[] permissionScopes)
    {
        using (var scope = app.Services.CreateScope())
        {
            await scope.ServiceProvider.EnsureScopes();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync(clientId) is not null)
            {
                return app;
            }

            OpenIddictApplicationDescriptor openIddictApplicationDescriptor = new()
            {
                ClientId = clientId,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                DisplayName = "RevrenLove Ledger Blazor WASM Client",
                ApplicationType = OpenIddictConstants.ClientTypes.Public,
                PostLogoutRedirectUris = { new Uri("https://localhost:7180/") },
                RedirectUris = { new Uri("https://localhost:7180/authentication/login-callback") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                }
            };

            foreach (var permissionScope in permissionScopes)
            {
                openIddictApplicationDescriptor.Permissions.Add(permissionScope);
            }

            // TODO: JE - Move all the magic strings to the config
            await manager.CreateAsync(openIddictApplicationDescriptor);
        }

        return app;
    }

    private static async Task EnsureScopes(this IServiceProvider services)
    {
        var manager = services.GetRequiredService<IOpenIddictScopeManager>();

        async Task CreateIfMissing(string name, string display)
        {
            if (await manager.FindByNameAsync(name) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = name,
                    DisplayName = display,
                    Resources = { "api" } // adjust as needed
                });
            }
        }

        await CreateIfMissing("openid", "OpenID");
        await CreateIfMissing("profile", "Profile");
        await CreateIfMissing("email", "Email");
        await CreateIfMissing("api", "API");
    }
}