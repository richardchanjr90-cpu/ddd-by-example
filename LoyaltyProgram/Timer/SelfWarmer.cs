using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Timer
{
    public static class SelfWarmer
    {
        [FunctionName("SelfWarmer")]
        public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"SelfWarmer executed: {DateTime.Now}");
        }
    }
}