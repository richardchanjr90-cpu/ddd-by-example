using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyProductGroups
{
    public interface ICreateLoyaltyProductNotificationHandler
        : INotificationHandler<CreateLoyaltyProductGroupNotification>
    {
    }
}
