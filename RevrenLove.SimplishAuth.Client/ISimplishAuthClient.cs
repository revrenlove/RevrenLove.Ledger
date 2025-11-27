namespace RevrenLove.SimplishAuth.Client;

public interface ISimplishAuthClient
{
    // POST
    // 200
    // 400 - HttpValidationProblemDetails
    Task<SimplishAuthClientResult> Register(RegisterRequest request);

    // POST
    // 200 - AccessTokenResponse
    // 401
    Task<SimplishAuthClientResult<AccessTokenResponse>> Login(LoginRequest request, bool useCookies = false, bool useSessionCookies = false);

    // POST
    // 200 - AccessTokenResponse
    // 401
    Task<SimplishAuthClientResult<AccessTokenResponse>> Refresh(RefreshRequest request);

    // POST
    // 200
    Task<SimplishAuthClientResult> ResendConfirmationEmail(ResendConfirmationEmailRequest request);

    // POST
    // 200
    // 400 - HttpValidationProblemDetails
    Task<SimplishAuthClientResult> ForgotPassword(ForgotPasswordRequest request);

    // POST
    // 200
    // 400 - HttpValidationProblemDetails
    Task<SimplishAuthClientResult> ResetPassword(ResetPasswordRequest request);

    // POST - Bearer
    // 200 - TwoFactorResponse
    // 400 - HttpValidationProblemDetails
    // 401
    // TODO: JE - This may need to be split up into multiple methods based on what this shit is intended to do...
    Task<SimplishAuthClientResult<TwoFactorResponse>> Manage2Fa(string bearerToken, TwoFactorRequest request);

    // GET - Bearer
    // 200 - InfoResponse
    // 400 - HttpValidationProblemDetails
    // 404
    Task<SimplishAuthClientResult<InfoResponse>> ManageInfo(string bearerToken);

    // POST - Bearer
    // 200 - InfoResponse
    // 400 - HttpValidationProblemDetails
    // 404
    Task<SimplishAuthClientResult<InfoResponse>> ManageInfo(string bearerToken, InfoRequest request);
}