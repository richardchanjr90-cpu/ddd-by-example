using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupNotification : INotification
    {
        public long Id { get; set; }
    }
}