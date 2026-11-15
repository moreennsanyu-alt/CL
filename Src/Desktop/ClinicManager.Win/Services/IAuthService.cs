namespace ClinicManager.Win.Services;

public enum LoginResult
{
    Success,
    InvalidCredentials,
    UserNotFound,
    AccountLocked,
    AccountDisabled,
    EmailNotVerified,
    PasswordExpired,
    TwoFactorRequired,
    TooManyAttempts,
    NetworkError,
    ServiceUnavailable,
    UnknownError
}

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
}

