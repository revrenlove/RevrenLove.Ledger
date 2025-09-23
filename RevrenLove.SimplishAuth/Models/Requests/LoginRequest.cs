namespace RevrenLove.SimplishAuth.Models.Requests;

//
// Summary:
//     The request type for the "/login" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).

public sealed class LoginRequest
{
    //
    // Summary:
    //     The user's email address which acts as a user name.
    public required string Email { get; init; }
    //
    // Summary:
    //     The user's password.
    public required string Password { get; init; }
    //
    // Summary:
    //     The optional two-factor authenticator code. This may be required for users who
    //     have enabled two-factor authentication. This is not required if a Microsoft.AspNetCore.Identity.Data.LoginRequest.TwoFactorRecoveryCode
    //     is sent.
    public string? TwoFactorCode { get; init; }
    //
    // Summary:
    //     An optional two-factor recovery code from Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.RecoveryCodes.
    //     This is required for users who have enabled two-factor authentication but lost
    //     access to their Microsoft.AspNetCore.Identity.Data.LoginRequest.TwoFactorCode.
    public string? TwoFactorRecoveryCode { get; init; }
}