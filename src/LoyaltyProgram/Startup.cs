using System;
using System.Reflection;
using AzureExtensions.FunctionToken.Extensions;
using AzureExtensions.FunctionToken.FunctionBinding.Options;
using AzureFunctions.Extensions.Swashbuckle;
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
                Audience = "zalik-243111",
                Issuer = "https://securetoken.google.com/zalik-243111",
                GoogleServiceAccountJsonUri = new Uri("https://secretstorage.blob.core.windows.net/firebase/zalik-243111-firebase-adminsdk-83897-987d10f2db.json?sp=r&st=2019-08-17T10:10:57Z&se=2099-08-17T18:10:57Z&spr=https&sv=2018-03-28&sig=REDACTED_SAS_SIG&sr=b")
            });

            if (Environment.GetEnvironmentVariable("FUNCTION_ENV") != "stage")
            {
                builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
            }
        }
    }
}