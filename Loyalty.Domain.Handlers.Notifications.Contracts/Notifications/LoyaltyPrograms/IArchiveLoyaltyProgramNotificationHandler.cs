using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyPrograms
{
    public interface IArchiveLoyaltyProgramNotificationHandler 
        : INotificationHandler<ArchiveLoyaltyProgramNotification>
    {
    }
}
