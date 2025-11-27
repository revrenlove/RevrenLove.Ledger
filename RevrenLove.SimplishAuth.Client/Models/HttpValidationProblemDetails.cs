namespace RevrenLove.SimplishAuth.Client.Models;

/// <summary>
/// A validation problem details object that conforms to RFC 7807.
/// </summary>
public class HttpValidationProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }

    /// <summary>
    /// Gets or sets the validation errors associated with this response.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(0);

    public HttpValidationProblemDetails() { }

    public HttpValidationProblemDetails(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}