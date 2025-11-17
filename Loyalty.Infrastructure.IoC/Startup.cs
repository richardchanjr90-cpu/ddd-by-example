using System;
using AzureExtensions.FunctionToken.Extensions;
using AzureExtensions.FunctionToken.FunctionBinding.Options;
using Loyalty.Infrastructure.IoC;
using Loyalty.Infrastructure.IoC.DI;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Loyalty.Infrastructure.IoC
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.SetupAppServices();
            builder.Services.SetupDb(config);
            builder.Services.SetupThirdParty();
            builder.Services.SetupServiceBus(config);
            builder.Services.SetupSettings(config);

            builder.AddAzureFunctionsToken(new TokenAzureB2COptions()
            {
                AzureB2CSingingKeyUri = new Uri("https://loyaltyprogramapp.b2clogin.com/loyaltyprogramapp.onmicrosoft.com/discovery/v2.0/keys?p=b2c_1_google-web-dev-policy"),
                Audience = "7dff948f-203c-452c-9f46-f8254cb61009",
                Issuer = "https://loyaltyprogramapp.b2clogin.com/73878e96-491d-4eac-97b2-5c77688cbbed/v2.0/"
            });
        }
    }
}