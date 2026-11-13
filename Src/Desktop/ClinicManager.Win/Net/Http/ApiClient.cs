using System.Net.Http.Json;

namespace ClinicManager.Win.Net.Http;

/// <summary>Typed HttpClient registered through IServiceCollection.AddHttpClient (see App.ConfigureServices).</summary>
public sealed class ApiClient(HttpClient httpClient) : IApiClient
{
    public Task<T?> GetAsync<T>(string uri, CancellationToken cancellationToken = default)
        => httpClient.GetFromJsonAsync<T>(uri, cancellationToken);
}
