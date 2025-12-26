using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyPrograms
{
    public interface ICreateLoyaltyProgramNotificationHandler
        : INotificationHandler<CreateLoyaltyProgramNotification>
    {
    }
}