using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Persistence;

internal static class ModelBuilderExtensions
{
    public static void ConfigureActivableEntities(this ModelBuilder builder)
    {
        var activableTypes =
            builder
                .Model
                .GetEntityTypes()
                .Where(t => typeof(IActivable).IsAssignableFrom(t.ClrType));

        foreach (var t in activableTypes)
        {
            builder
                .Entity(t.ClrType)
                .HasQueryFilter(BuildIsActiveFilter(t.ClrType))
                .Property(nameof(IActivable.IsActive))
                .HasDefaultValue(true);
        }
    }

    private static LambdaExpression BuildIsActiveFilter(Type entityType)
    {
        var param = Expression.Parameter(entityType, "e");
        var prop = Expression.Property(param, nameof(IActivable.IsActive));
        var condition = Expression.Equal(prop, Expression.Constant(true));
        var lambda = Expression.Lambda(condition, param);

        return lambda;
    }
}
