namespace RevrenLove.Ledger.Api.Configuration;

public class AppSettings
{
    public required string AllowedHosts { get; set; }
    public required ConnectionStrings ConnectionStrings { get; set; }
    public required Jwt Jwt { get; set; }
    public required Authentication Authentication { get; set; }
}
