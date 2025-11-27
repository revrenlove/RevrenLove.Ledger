namespace RevrenLove.SimplishAuth.Client.Models.Requests;

//
// Summary:
//     The request type for the "/refresh" endpoint added by Microsoft.AspNetCore.Routing.IdentityApiEndpointRouteBuilderExtensions.MapIdentityApi``1(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder).
public sealed class RefreshRequest
{
    //
    // Summary:
    //     The Microsoft.AspNetCore.Authentication.BearerToken.AccessTokenResponse.RefreshToken
    //     from the last "/login" or "/refresh" response used to get a new Microsoft.AspNetCore.Authentication.BearerToken.AccessTokenResponse
    //     with an extended expiration.
    public required string RefreshToken { get; init; }
}