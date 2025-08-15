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

                services.AddScoped<ILoyaltyDbContext, LoyaltyDbContext>();

                var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];

                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<LoyaltyDbContext>(
                        options => options.UseSqlServer(
                            connectionString
                        ));

                services.Configure<AuthSettings>(options =>
                    hostContext.Configuration.GetSection(nameof(AuthSettings)).Bind(options));
            });

            return builder;
        }
    }
}
