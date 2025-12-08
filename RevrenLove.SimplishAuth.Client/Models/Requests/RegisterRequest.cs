namespace RevrenLove.SimplishAuth.Client;

//
// Summary:
//     The request type for the "/register" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class RegisterRequest
{
    //
    // Summary:
    //     The user's email address which acts as a user name.
    public required string Email { get; init; }
    //
    // Summary:
    //     The user's password.
    public required string Password { get; init; }
}
