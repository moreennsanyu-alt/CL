namespace ClinicManager.Core.Services;

public interface IAuthService
{
    Task<(LoginResult, string)> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
}
