#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace RevrenLove.Ledger.Services;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class UniqueConstraintException(string message) : Exception(message)
{
}
