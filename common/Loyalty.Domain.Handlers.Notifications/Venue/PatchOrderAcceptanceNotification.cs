using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchOrderAcceptanceNotification: IIntegrationEventsNotification
    {
        public long VenueId { get; set; }

        public bool Accept { get; set; }
    }
}
