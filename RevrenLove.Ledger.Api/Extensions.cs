#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class Extensions
{
    public static IServiceCollection AddLedgerApiCors(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment())
        {
            services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
        }
        else
        {
            // TODO: JE - Actually configure the policy for production
        }

        return services;
    }
}
