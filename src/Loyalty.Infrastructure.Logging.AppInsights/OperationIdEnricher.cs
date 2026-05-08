using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Loyalty.Infrastructure.Logging.AppInsights
{
    public class OperationIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var activity = Activity.Current;

            if (activity is null)
            {
                return;
            }

            logEvent.AddPropertyIfAbsent(new LogEventProperty("Operation Id", new ScalarValue(activity.Id)));
            logEvent.AddPropertyIfAbsent(new LogEventProperty("Parent Id", new ScalarValue(activity.ParentId)));
        }
    }
}
