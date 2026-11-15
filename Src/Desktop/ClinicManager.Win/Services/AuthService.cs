namespace ClinicManager.Win.Services;

public class DummyAuthService : IAuthService
{
    public async Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return LoginResult.Success;
    }
}
