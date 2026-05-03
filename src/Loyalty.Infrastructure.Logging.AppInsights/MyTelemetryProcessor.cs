using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Loyalty.Infrastructure.Logging.AppInsights
{
    public class MyTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor next;

        public MyTelemetryProcessor(ITelemetryProcessor next)
        {
            this.next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item is DependencyTelemetry request && request.Duration.Milliseconds < 200)
            {
                return;
            }

            this.next.Process(item);
        }
    }
}
