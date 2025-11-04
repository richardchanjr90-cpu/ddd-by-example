using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class CreateLoyaltyProductGroupNotification : INotification
    {
        public long Id { get; set; }

        public string GroupName { get; set; }

        public string Rule { get; set; }
    }
}
