using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Domain.Handlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureData(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<IMongoDataClient, MongoDataClient>();
                services.AddMediatR(typeof(BaseHandler).Assembly);
                services.Configure<DbSettings>(options =>
                    hostContext.Configuration.GetSection(nameof(DbSettings)).Bind(options));
            });

            return builder;
        }
    }
}
