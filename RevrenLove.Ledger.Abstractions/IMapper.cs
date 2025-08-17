namespace RevrenLove.Ledger.Abstractions;

public interface IMapper
{
    TDestination Map<TDestination, TSource>(TSource source);
    TDestination Map<TDestination>(object source);
}
