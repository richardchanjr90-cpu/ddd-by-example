using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Purchases
{
    public interface ICreatePurchaseNotificationHandler : INotificationHandler<CreatePurchaseNotification>
    {
    }
}
