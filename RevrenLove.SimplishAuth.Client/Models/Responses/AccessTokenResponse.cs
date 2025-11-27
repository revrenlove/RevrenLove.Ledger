namespace RevrenLove.SimplishAuth.Client.Models.Responses;

//
// Summary:
//     The JSON data transfer object for the bearer token response typically found in
//     "/login" and "/refresh" responses.
public sealed class AccessTokenResponse
{
    //
    // Summary:
    //     The value is always "Bearer" which indicates this response provides a "Bearer"
    //     token in the form of an opaque Microsoft.AspNetCore.Authentication.BearerToken.AccessTokenResponse.AccessToken.
    //
    //
    // Remarks:
    //     This is serialized as "tokenType": "Bearer" using System.Text.Json.JsonSerializerDefaults.Web.
    public string TokenType { get; } = "Bearer";
    //
    // Summary:
    //     The opaque bearer token to send as part of the Authorization request header.
    //
    //
    // Remarks:
    //     This is serialized as "accessToken": "{AccessToken}" using System.Text.Json.JsonSerializerDefaults.Web.
    public required string AccessToken { get; init; }
    //
    // Summary:
    //     The number of seconds before the Microsoft.AspNetCore.Authentication.BearerToken.AccessTokenResponse.AccessToken
    //     expires.
    //
    // Remarks:
    //     This is serialized as "expiresIn": "{ExpiresInSeconds}" using System.Text.Json.JsonSerializerDefaults.Web.
    public required long ExpiresIn { get; init; }
    //
    // Summary:
    //     If set, this provides the ability to get a new access_token after it expires
    //     using a refresh endpoint.
    //
    // Remarks:
    //     This is serialized as "refreshToken": "{RefreshToken}" using using System.Text.Json.JsonSerializerDefaults.Web.
    public required string RefreshToken { get; init; }
}