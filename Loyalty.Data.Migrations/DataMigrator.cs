using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Loyalty.Data.Migrations
{
    public class DataMigrator
    {
        private static readonly SortedList<int, Func<IMongoCollection<Venue>, bool>> IndexActions =
            new SortedList<int, Func<IMongoCollection<Venue>, bool>>();

        private static readonly object LockObject = new object();

        private static bool wasExecuted;

        static DataMigrator()
        {
            IndexActions.Add(0, CreateVenueGuidIndexIfExists);
        }

        public static void MigrateData(IServiceProvider provider)
        {
            if (!wasExecuted)
            {
                lock (LockObject)
                {
                    if (!wasExecuted)
                    {
                        RunActualMigrations(provider);
                        wasExecuted = true;
                    }
                }
            }
        }

        private static void RunActualMigrations(IServiceProvider provider)
        {
            var client = provider.GetService<IMongoDataClient>();
            using (var scope = provider.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var db = client.Client.GetDatabase(config[$"{nameof(DbSettings)}:{nameof(DbSettings.DatabaseName)}"]);

                IMongoCollection<Venue> collection = db.GetCollection<Venue>(nameof(Venue));

                for (int i = IndexActions.Count - 1; i >= 0; --i)
                {
                    var result = IndexActions[i]?.Invoke(collection);
                    if (result.HasValue && result.Value)
                    {
                        break;
                    }
                }
            }
        }

        #region Index

        private static bool CreateVenueGuidIndexIfExists(IMongoCollection<Venue> collection)
        {
            var builder = Builders<Venue>.IndexKeys;
            var keys = builder.Hashed(x => x.ItemId);

            var indexModel = new CreateIndexModel<Venue>(keys);
            var result = collection.Indexes.CreateOne(indexModel);
            return false;
        }

        #endregion

    }
}
