using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Venues
{
    public interface IPatchVenueApproveNotificationHandler 
        : INotificationHandler<PatchVenueApproveNotification>
    {
    }
}
