using System;
using System.Diagnostics;
using AzureExtensions.FunctionToken.Extensions;
using AzureExtensions.FunctionToken.FunctionBinding.Options;
using AzureFunctions.Extensions.NotificationHubs.Extensions;
using Loyalty.Common.Shared.Settings;
using LoyaltyProgram;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;

[assembly: WebJobsStartup(typeof(WebJobsStartup))]
namespace LoyaltyProgram
{
    public class WebJobsStartup : IWebJobsStartup
    {
        void IWebJobsStartup.Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            builder.AddAzureFunctionsToken(new FireBaseOptions
            {
                Audience = config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.ProjectName)}"],
                Issuer = config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.Issuer)}"],
                GoogleServiceAccountJsonUri = new Uri(config[$"{nameof(GoogleAuthSettings)}:{nameof(GoogleAuthSettings.JsonUri)}"])
            });

            builder.AddNotificationHubs();
        }
    }
}
