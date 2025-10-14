using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Timer
{
    public static class SelfWarmer
    {
        [FunctionName("SelfWarmer")]
        public static void Run([TimerTrigger("* /5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"SelfWarmer executed: {DateTime.Now}");
        }
    }
}
