using Loyalty.Domain.Handlers.Notifications.Purchases;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Purchases
{
    public interface IBurnPurchaseNotificationHandler : INotificationHandler<BurnPurchaseNotification>
    {
    }
}
