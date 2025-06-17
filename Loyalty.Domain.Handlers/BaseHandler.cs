using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Loyalty.Domain.Handlers
{
    public abstract class BaseHandler
    {
        protected BaseHandler(IMongoDataClient dbClient, IOptions<DbSettings> settings)
        {
            DbClient = dbClient;
            Database = dbClient.Client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoDataClient DbClient { get; }

        public IMongoDatabase Database { get; set; }
    }
}
