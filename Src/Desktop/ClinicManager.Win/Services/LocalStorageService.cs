using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClinicManager.Win.Services
{
    public sealed class LocalStorageService : ILocalStorageService
    {
        private readonly string _folder;
        private readonly byte[] _entropy;
   
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


        public async Task SaveAsync<T>(string key, T value, CancellationToken ct = default(CancellationToken))
        {
            var path = GetPath(key);
            var json = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, Formatting.None));
            var encrypted = ProtectedData.Protect(json, _entropy, DataProtectionScope.CurrentUser);

            // Write to temp file then swap, so a crash can't leave a corrupt file.
            var tmp = path + ".tmp";
            await WriteAllBytesAsync(tmp, encrypted, ct).ConfigureAwait(false);

            if (File.Exists(path))
                File.Replace(tmp, path, null);
            else
                File.Move(tmp, path);
        }

        public async Task<T> ReadAsync<T>(string key, CancellationToken ct = default(CancellationToken))
        {
            var path = GetPath(key);
            if (!File.Exists(path))
                return default(T);

            var encrypted = await ReadAllBytesAsync(path, ct).ConfigureAwait(false);
            try
            {
                var json = ProtectedData.Unprotect(encrypted, _entropy, DataProtectionScope.CurrentUser);
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(json));
            }
            catch (CryptographicException)
            {
                // Different user/machine, wrong entropy, or corrupted data.
                return default(T);
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

        public bool Exists(string key)
        {
            return File.Exists(GetPath(key));
        }

        private string GetPath(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("Key must be a valid file name.", nameof(key));

            return Path.Combine(_folder, key + ".dat");
        }

        // File.WriteAllBytesAsync / ReadAllBytesAsync don't exist in .NET Framework.
        private static async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken ct)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None,
                                           4096, useAsync: true))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length, ct).ConfigureAwait(false);
            }
        }

        private static async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken ct)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read,
                                           4096, useAsync: true))
            {
                var buffer = new byte[fs.Length];
                var offset = 0;
                while (offset < buffer.Length)
                {
                    var read = await fs.ReadAsync(buffer, offset, buffer.Length - offset, ct)
                                       .ConfigureAwait(false);
                    if (read == 0) break;
                    offset += read;
                }
                return buffer;
            }
        }
    }
}
