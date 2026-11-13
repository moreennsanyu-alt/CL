namespace ClinicManager.Win.Net.Http;

public interface IApiClient
{
    Task<T?> GetAsync<T>(string uri, CancellationToken cancellationToken = default);
}
