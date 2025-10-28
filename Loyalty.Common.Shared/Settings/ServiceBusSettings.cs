using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Common.Shared.Settings
{
    public class ServiceBusSettings
    {
        public string ConnectionString { get; set; }

        public string BusinessActivitiesQueueName { get; set; }
    }
}
