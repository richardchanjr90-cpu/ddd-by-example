using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyProductGroups
{
    public interface IUpdateLoyaltyProductNotificationHandler
        : INotificationHandler<UpdateLoyaltyProductGroupNotification>
    {
    }
}