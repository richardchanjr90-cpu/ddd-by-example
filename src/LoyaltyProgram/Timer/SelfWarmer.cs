using System;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Timer
{
    public class SelfWarmer
    {
        private readonly LoyaltyDbContext context;

        public SelfWarmer(LoyaltyDbContext context)
        {
            this.context = context;
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
        }
    }
}