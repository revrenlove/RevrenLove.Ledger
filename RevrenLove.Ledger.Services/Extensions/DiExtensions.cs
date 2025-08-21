using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Abstractions.Services;
using Entities = RevrenLove.Ledger.Entities;
using Models = RevrenLove.Ledger.Models;
using RevrenLove.Ledger.Services;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class DiExtensions
{
    public static IServiceCollection AddLedgerServices(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IDataAccessor<>), typeof(DataAccessor<>))
            .AddScoped<IFinancialAccountHolderService, FinancialAccountHolderService>()
            .AddLedgerMappings(cfg =>
            {
                cfg.AddMapping<Models.FinancialAccountHolder, Entities.FinancialAccountHolder>(
                    model =>
                        new()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Description = model.Description,
                            IsActive = true,
                        },
                    entity =>
                        new()
                        {
                            Id = entity.Id,
                            Name = entity.Name,
                            Description = entity.Description,
                        });
            });

        return services;
    }
}
