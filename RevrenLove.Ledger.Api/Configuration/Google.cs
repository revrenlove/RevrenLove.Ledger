namespace RevrenLove.Ledger.Api.Configuration;

public class Google
{
    public required string ClientId { get; set; }
    public required string ProjectId { get; set; }
    public required string AuthUri { get; set; }
    public required string TokenUri { get; set; }
    public required string AuthProviderX509CertUUrl { get; set; }
    public required string ClientSecret { get; set; }
    public required string[] RedirectUris { get; set; }
    public required string[] JavascriptOrigins { get; set; }
}
