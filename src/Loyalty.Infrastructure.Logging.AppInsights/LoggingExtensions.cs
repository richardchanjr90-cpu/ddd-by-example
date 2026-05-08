using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;

namespace Loyalty.Infrastructure.Logging.AppInsights
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration WithOperationId(this LoggerEnrichmentConfiguration enrichConfiguration)
        {
            if (enrichConfiguration is null)
            {
                throw new ArgumentNullException(nameof(enrichConfiguration));
            }

            return enrichConfiguration.With<OperationIdEnricher>();
        }

        public static ITelemetryConverter OperationIdEvents
        {
            get
            {
                return (ITelemetryConverter) new OperationTelemetryConverter();
            }
        }
    }
}
