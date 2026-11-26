using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ClinicManager.Win.Services;

public sealed class LocalStorageService : ILocalStorageService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = false
    };

    private readonly string _folder;
    private readonly byte[]? _entropy;

    public LocalStorageService(IOptions<ApplicationConfiguration> options)
    {
        var config = options.Value;

        _folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            config.ApplicationName, "storage");

        _entropy = string.IsNullOrEmpty(config.StorageEntropy)
            ? null
            : Encoding.UTF8.GetBytes(config.StorageEntropy);

        Directory.CreateDirectory(_folder);
    }

    public async Task SaveAsync<T>(string key, T value, CancellationToken ct = default)
    {
        var path = GetPath(key);
        var json = JsonSerializer.SerializeToUtf8Bytes(value, JsonOptions);
        var encrypted = ProtectedData.Protect(json, _entropy, DataProtectionScope.CurrentUser);

        // Write to temp file then move, so a crash can't leave a corrupt file.
        var tmp = path + ".tmp";
        await File.WriteAllBytesAsync(tmp, encrypted, ct);
        File.Move(tmp, path, overwrite: true);
    }

    public async Task<T?> ReadAsync<T>(string key, CancellationToken ct = default)
    {
        var path = GetPath(key);
        if (!File.Exists(path))
            return default;

        var encrypted = await File.ReadAllBytesAsync(path, ct);
        try
        {
            var json = ProtectedData.Unprotect(encrypted, _entropy, DataProtectionScope.CurrentUser);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
        catch (CryptographicException)
        {
            // Different user/machine, wrong entropy, or corrupted data.
            return default;
        }
    }

    public bool Delete(string key)
    {
        var path = GetPath(key);
        if (!File.Exists(path))
            return false;

        File.Delete(path);
        return true;
    }

    public bool Exists(string key) => File.Exists(GetPath(key));

    private string GetPath(string key)
    {
        if (string.IsNullOrWhiteSpace(key) || key.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            throw new ArgumentException("Key must be a valid file name.", nameof(key));

        return Path.Combine(_folder, key + ".dat");
    }
}
