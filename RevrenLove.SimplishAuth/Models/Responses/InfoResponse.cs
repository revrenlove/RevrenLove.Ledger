namespace RevrenLove.SimplishAuth.Models.Responses;

//
// Summary:
//     The response type for the "/manage/info" endpoints added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class InfoResponse
{
    //
    // Summary:
    //     The email address associated with the authenticated user.
    public required string Email { get; init; }
    //
    // Summary:
    //     Indicates whether or not the Microsoft.AspNetCore.Identity.Data.InfoResponse.Email
    //     has been confirmed yet.
    public required bool IsEmailConfirmed { get; init; }
}