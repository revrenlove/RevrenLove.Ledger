using RevrenLove.SimplishAuth.Models.Requests;
using RevrenLove.SimplishAuth.Models.Responses;

namespace RevrenLove.SimplishAuth;

public interface ISimplishAuthClient
{
    /// <summary>
    /// Registers a user with the given credentials.
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <exception cref="ValidationException">Thrown when <paramref name="registerRequest"/> has an invalid email or password.</exception>
    Task Register(RegisterRequest registerRequest);

    // POST
    // 200 - AccessTokenResponse
    // 401
    Task<AccessTokenResponse> Login(LoginRequest loginRequest, bool? useCookies = null, bool? useSessionCookies = null);

    // POST
    // 200 - AccessTokenResponse
    // 401
    Task<AccessTokenResponse> Refresh(RefreshRequest refreshRequest);

    // POST
    // 200
    Task ResendConfirmationEmail(ResendConfirmationEmailRequest resendConfirmationEmailRequest);

    // POST
    // 200
    // 400 - HttpValidationProblemDetails
    Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);

    // POST
    // 200
    // 400 - HttpValidationProblemDetails
    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);

    // POST - Bearer
    // 200 - TwoFactorResponse
    // 400 - HttpValidationProblemDetails
    // 401
    // TODO: JE - This may need to be split up into multiple methods based on what this shit is intended to do...
    Task<TwoFactorResponse> Manage2Fa(string bearerToken, TwoFactorRequest twoFactorRequest);

    // GET - Bearer
    // 200 - InfoResponse
    // 400 - HttpValidationProblemDetails
    // 404
    Task<InfoResponse> ManageInfo(string bearerToken);

    // POST - Bearer
    // 200 - InfoResponse
    // 400 - HttpValidationProblemDetails
    // 404
    Task<InfoResponse> ManageInfo(string bearerToken, InfoRequest infoRequest);
}