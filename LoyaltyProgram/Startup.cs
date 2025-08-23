using System;
using AutoMapper;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Domain.AutoMapper;
using Loyalty.Domain.Handlers;
using Loyalty.Venue.Service;
using LoyaltyProgram;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace LoyaltyProgram
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) =>
            builder.AddDependencyInjection(ConfigureServices);

        private void ConfigureServices(IServiceCollection services)
        {
            //put in a single place and refactor;
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddScoped<LoyaltyVenueAppService>();
            services.AddMediatR(typeof(BaseHandler).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<ILoyaltyDbContext, LoyaltyDbContext>();

            var serviceProvider = services.BuildServiceProvider();
         
            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<LoyaltyDbContext>(
                    options => options.UseSqlServer(
                        connectionString));

            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            services.Configure<AuthSettings>(options => config.GetSection(nameof(AuthSettings)).Bind(options));
        }
    }
}
