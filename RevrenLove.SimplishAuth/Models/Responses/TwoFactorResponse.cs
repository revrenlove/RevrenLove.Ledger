namespace RevrenLove.SimplishAuth.Models.Responses;

//
// Summary:
//     The response type for the "/manage/2fa" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class TwoFactorResponse
{
    //
    // Summary:
    //     The shared key generally for TOTP authenticator apps that is usually presented
    //     to the user as a QR code.
    public required string SharedKey { get; init; }
    //
    // Summary:
    //     The number of unused Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.RecoveryCodes
    //     remaining.
    public required int RecoveryCodesLeft { get; init; }
    //
    // Summary:
    //     The recovery codes to use if the Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.SharedKey
    //     is lost. This will be omitted from the response unless Microsoft.AspNetCore.Identity.Data.TwoFactorRequest.ResetRecoveryCodes
    //     was set or two-factor was enabled for the first time.
    public string[]? RecoveryCodes { get; init; }
    //
    // Summary:
    //     Whether or not two-factor login is required for the current authenticated user.
    public required bool IsTwoFactorEnabled { get; init; }
    //
    // Summary:
    //     Whether or not the current client has been remembered by two-factor authentication
    //     cookies. This is always false for non-cookie authentication schemes.
    public required bool IsMachineRemembered { get; init; }
}