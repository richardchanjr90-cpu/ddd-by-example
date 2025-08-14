using AutoMapper;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Domain.AutoMapper;
using Loyalty.Domain.Handlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                services.AddMediatR(typeof(BaseHandler).Assembly);
                services.AddAutoMapper(typeof(AutoMapperProfile));

                services.AddTransient<ILoyaltyDbContext, LoyaltyDbContext>();

                var config = builder.Build().Services.GetRequiredService<IConfiguration>();
                var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.DatabaseName)}"];

                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<LoyaltyDbContext>(
                        options => options.UseSqlServer(
                            connectionString
                        ));

                services.Configure<DbSettings>(options =>
                    hostContext.Configuration.GetSection(nameof(DbSettings)).Bind(options));

                services.Configure<AuthSettings>(options =>
                    hostContext.Configuration.GetSection(nameof(AuthSettings)).Bind(options));
            });

            return builder;
        }
    }
}
