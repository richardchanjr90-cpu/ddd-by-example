using System;
using MongoDB.Driver;

namespace Loyalty.Data.Contracts
{
    public interface IMongoDataClient
    {
        MongoClient Client { get; }
    }
}
