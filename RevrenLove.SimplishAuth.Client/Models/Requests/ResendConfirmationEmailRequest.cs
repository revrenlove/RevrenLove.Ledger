namespace RevrenLove.SimplishAuth.Client;

//
// Summary:
//     The response type for the "/resendConfirmationEmail" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class ResendConfirmationEmailRequest
{
    //
    // Summary:
    //     The email address to resend the confirmation email to if a user with that email
    //     exists.
    public required string Email { get; init; }
}