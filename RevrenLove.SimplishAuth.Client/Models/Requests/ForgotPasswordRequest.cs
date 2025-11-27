namespace RevrenLove.SimplishAuth.Client.Models.Requests;

//
// Summary:
//     The response type for the "/forgotPassword" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class ForgotPasswordRequest
{
    //
    // Summary:
    //     The email address to send the reset password code to if a user with that confirmed
    //     email address already exists.
    public required string Email { get; init; }
}
