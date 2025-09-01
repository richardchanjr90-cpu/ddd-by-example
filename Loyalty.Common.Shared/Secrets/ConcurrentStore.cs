using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Loyalty.Common.Shared.Secrets
{
    public sealed class ConcurrentStore
    {
        private readonly string keyVaultUrl;
        private static readonly ConcurrentDictionary<string, string> CachedDictionary = new ConcurrentDictionary<string, string>();

        //This client is located in a shared lib and is static 
        //to share it between functions and between instances of those functions.
        private static readonly HttpClient SharedClient = new HttpClient();

        public ConcurrentStore(string keyVaultUrl)
        {
            this.keyVaultUrl = keyVaultUrl;
        }

        private static readonly Lazy<KeyVaultClient> VaultClient = new Lazy<KeyVaultClient>(GetStore);

        public async Task<string> GetOrLoadSettingAsync(string secretId)
        {
            if (!CachedDictionary.TryGetValue(secretId, out var value))
            {
                string tableString = (await VaultClient.Value.GetSecretAsync(keyVaultUrl, secretId)).Value;
                CachedDictionary.TryAdd(secretId, tableString);
                value = tableString;
            }

            return value;
        }

        private static KeyVaultClient GetStore()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback),
                SharedClient);
            return keyVaultClient;
        }
    }
}
;