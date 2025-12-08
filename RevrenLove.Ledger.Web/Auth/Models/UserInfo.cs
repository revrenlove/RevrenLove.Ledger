// TODO: JE - This is only used in the example I'm basing shit off of. Remove it.

namespace RevrenLove.Ledger.Web.Auth.Models;

/// <summary>
/// User info from identity endpoint to establish claims.
/// </summary>
public class UserInfo
{
    /// <summary>
    /// The email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// A value indicating whether the email has been confirmed yet.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }

    /// <summary>
    /// The list of claims for the user.
    /// </summary>
    public Dictionary<string, string> Claims { get; set; } = [];
}
