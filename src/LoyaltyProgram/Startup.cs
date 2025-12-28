using System;
using System.Reflection;
using AzureExtensions.FunctionToken.Extensions;
using AzureExtensions.FunctionToken.FunctionBinding.Options;
using AzureFunctions.Extensions.Swashbuckle;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.IoC.DI;
using LoyaltyProgram;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace LoyaltyProgram
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
            builder.Services.AddHttpContextAccessor();

            builder.AddAzureFunctionsToken(new FireBaseOptions
            {
                Audience = config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.ProjectName)}"],
                Issuer = config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.Issuer)}"],
                GoogleServiceAccountJsonUri = new Uri(config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.JsonUri)}"])
            });

            if (Environment.GetEnvironmentVariable("FUNCTION_ENV") != "stage")
            {
                builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
            }
        }
    }
}