using System;
using System.Security.Authentication;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Loyalty.Data.DataAccess
{
    public class MongoDataClient : IMongoDataClient
    {
        private const string CredentialMechanism = "SCRAM-SHA-1";

        public MongoClient Client { get; }

        public MongoDataClient(IOptions<DbSettings> settings)
        {
            var mongoSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(settings.Value.Host, settings.Value.Port),
                UseSsl = settings.Value.UseSsl,
                SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 }
            };

            var identity = new MongoInternalIdentity(settings.Value.DatabaseName, settings.Value.UserName);
            var evidence = new PasswordEvidence(settings.Value.Password);

            mongoSettings.Credential = new MongoCredential(CredentialMechanism, identity, evidence);

            Client = new MongoClient(mongoSettings);
        }
    }
}
