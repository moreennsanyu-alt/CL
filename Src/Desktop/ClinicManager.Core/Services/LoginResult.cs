namespace ClinicManager.Core.Services;

public enum LoginResult
{
    Success,
    InvalidCredentials,
    UserNotFound,
    LockedOut,
    AccountDisabled,
    EmailNotConfirmed,
    PasswordExpired,
    TwoFactorRequired,
    NetworkError,
    Timeout,
    ServiceUnavailable,
    Unknown
}
