using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Rate
{
    public class UpsertUserRateNotification: IUserEventsNotification
    {
        public string UserId { get; set; }

        public long VenueId { get; set; }

        public long OrderId { get; set; }

        public int Rate { get; set; }
    }
}
