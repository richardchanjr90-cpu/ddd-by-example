using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Venues
{
    public interface ICreateVenueNotificationHandler : INotificationHandler<CreateVenueNotification>
    {
    }
}
