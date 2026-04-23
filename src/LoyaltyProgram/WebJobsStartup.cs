using System;
using System.Diagnostics;
using System.Linq;
using AzureExtensions.FunctionToken.Extensions;
using AzureExtensions.FunctionToken.FunctionBinding.Options;
using AzureFunctions.Extensions.NotificationHubs.Extensions;
using Loyalty.Common.Shared.Settings;
using LoyaltyProgram;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            builder.Services.AddHttpContextAccessor();
            var configDescriptor = builder.Services.SingleOrDefault(tc => tc.ServiceType == typeof(TelemetryConfiguration));

            var implFactory = configDescriptor?.ImplementationFactory;

            if (implFactory != null)
            {
                builder.Services.Remove(configDescriptor);
                builder.Services.AddSingleton(provider =>
                {
                    if (implFactory.Invoke(provider) is TelemetryConfiguration telemetryConfiguration)
                    {
                        var httpAccessor = provider.GetService<IHttpContextAccessor>();

                        telemetryConfiguration.TelemetryInitializers.Add(new TelemetryInitializer(httpAccessor));
                        telemetryConfiguration.TelemetryProcessorChainBuilder.Use(next => new MyTelemetryProcessor(next));
                        telemetryConfiguration.TelemetryProcessorChainBuilder.Build();

                        return telemetryConfiguration;
                    }
                    return null;
                });
            }
        }
    }
}
