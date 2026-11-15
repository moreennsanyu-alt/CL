using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ClinicManager.Win.Services;

public interface ILocalStorageService
{
    Task SaveAsync<T>(string key, T value, CancellationToken ct = default);
    Task<T?> ReadAsync<T>(string key, CancellationToken ct = default);
    bool Delete(string key);
    bool Exists(string key);
}
