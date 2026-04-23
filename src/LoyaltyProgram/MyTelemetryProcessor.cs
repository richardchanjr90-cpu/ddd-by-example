using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace LoyaltyProgram
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
            var request = item as DependencyTelemetry;
            
            if (request != null && request.Duration.Milliseconds < 200)
            {
                return;
            }

            this.next.Process(item);
        }
    }
}
