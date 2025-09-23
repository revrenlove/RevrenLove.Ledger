namespace RevrenLove.SimplishAuth.Models.Requests;

//
// Summary:
//     The request type for the "/manage/2fa" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class TwoFactorRequest
{
    //
    // Summary:
    //     An optional System.Boolean to enable or disable the two-factor login requirement
    //     for the authenticated user. If null or unset, the current two-factor login requirement
    //     for the user will remain unchanged.
    public bool? Enable { get; init; }
    //
    // Summary:
    //     The two-factor code derived from the Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.SharedKey.
    //     This is only required if Microsoft.AspNetCore.Identity.Data.TwoFactorRequest.Enable
    //     is set to true.
    public string? TwoFactorCode { get; init; }
    //
    // Summary:
    //     An optional System.Boolean to reset the Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.SharedKey
    //     to a new random value if true. This automatically disables the two-factor login
    //     requirement for the authenticated user until it is re-enabled by a later request.
    public bool ResetSharedKey { get; init; }
    //
    // Summary:
    //     An optional System.Boolean to reset the Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.RecoveryCodes
    //     to new random values if true. Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.RecoveryCodes
    //     will be empty unless they are reset or two-factor was enabled for the first time.
    public bool ResetRecoveryCodes { get; init; }
    //
    // Summary:
    //     An optional System.Boolean to clear the cookie "remember me flag" if present.
    //     This has no impact on non-cookie authentication schemes.
    public bool ForgetMachine { get; init; }
}
