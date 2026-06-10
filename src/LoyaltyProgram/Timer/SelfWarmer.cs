using System;
using Loyalty.Infrastructure.DataAccess.Context;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Timer
{
    public class SelfWarmer
    {
        private readonly LoyaltyDbContext context;
        private readonly IntegrationEventsContext eventsContext;

        public SelfWarmer(LoyaltyDbContext context, IntegrationEventsContext eventsContext)
        {
            this.context = context;
            this.eventsContext = eventsContext;
        }

        [FunctionName("SelfWarmer")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"SelfWarmer executed: {DateTime.Now}");

            using (context)
            {
                //force the model creation
                var model = context.Model; 
                Console.WriteLine(model);
            }

            using (eventsContext)
            {
                //force the model creation
                var model = eventsContext.Model; 
                Console.WriteLine(model);
            }
        }
    }
}