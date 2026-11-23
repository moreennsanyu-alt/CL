namespace ClinicManager.Core.Services;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
}
