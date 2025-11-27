namespace RevrenLove.SimplishAuth.Client;

//
// Summary:
//     The response type for the "/resetPassword" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
//     The "/resetPassword" endpoint requires the "/forgotPassword" endpoint to be called
//     first to get the Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest.ResetCode.
public sealed class ResetPasswordRequest
{
    //
    // Summary:
    //     The email address for the user requesting a password reset. This should match
    //     Microsoft.AspNetCore.Identity.Data.ForgotPasswordRequest.Email.
    public required string Email { get; init; }
    //
    // Summary:
    //     The code sent to the user's email to reset the password. To get the reset code,
    //     first make a "/forgotPassword" request.
    public required string ResetCode { get; init; }
    //
    // Summary:
    //     The new password the user with the given Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest.Email
    //     should login with. This will replace the previous password.
    public required string NewPassword { get; init; }
}
