using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RevrenLove.Ledger.Entities;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class Extensions
{
    public static IServiceCollection AddLedgerApiCors(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
    {
        // if (webHostEnvironment.IsDevelopment())
        // {
        services
            .AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        // TODO: JE - Make this part of the appSettings...
                        .WithOrigins("https://localhost:5088")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        // }
        // else
        // {
        // TODO: JE - Actually configure the policy for production
        // }

        return services;
    }

    public static Guid GetUserIdAsGuid(this UserManager<LedgerUser> userManager, ClaimsPrincipal user)
    {
        return Guid.Parse(userManager.GetUserId(user)!);
    }
}
