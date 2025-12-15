namespace RevrenLove.Ledger.Services;

public class NotFoundException(string message, Exception innerException) : Exception(message, innerException)
{
}
