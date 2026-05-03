using System;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.IoC;
using Loyalty.Infrastructure.IoC.DI;
using LoyaltyProgram;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

[assembly: FunctionsStartup(typeof(Startup))]
namespace LoyaltyProgram
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
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

            builder.Services.Decorate(
                typeof(INotificationHandler<>), 
                typeof(TransactionalNotificationDecorator<>));

            builder.AddSwashBuckle(
                Assembly.GetExecutingAssembly(), opts =>
                {
                    opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
                    opts.AddCodeParameter = false;
                    opts.PrependOperationWithRoutePrefix = true;
                    opts.XmlPath = "TestFunction.xml";
                    opts.Documents = new[]
                    {
                        new SwaggerDocument
                        {
                            Name = "v1",
                            Title = "Swagger document",
                            Description = "Swagger document",
                            Version = "v2"
                        }
                    };
                    opts.Title = "Swagger";
                    opts.OverridenPathToSwaggerJson = new Uri(config[$"{nameof(SwaggerSettings)}:{nameof(SwaggerSettings.SwaggerJsonUri)}"]);
                });
        }
    }
}