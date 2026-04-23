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
            //if (item is RequestTelemetry request)
            //{
            //    if (request.ResponseCode == "200")
            //    {
            //        return;
            //    }
            //}
            this.next.Process(item);
        }
    }
}
