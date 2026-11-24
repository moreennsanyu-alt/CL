namespace ClinicManager.Win.Services;

public class AuthService(IOptions<ApplicationConfiguration> options) : IAuthService
{
    public async Task<(LoginResult Result, string Message)> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return (LoginResult.Success,"Logged in successfully.");
    }
}
