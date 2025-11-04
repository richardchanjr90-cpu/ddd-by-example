using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class UpdateLoyaltyProductGroupNotification : INotification
    {
        public long Id { get; set; }

        public string GroupName { get; set; }

        public string Rule { get; set; }

        public int RuleType { get; set; }

        public decimal Total { get; set; }
    }
}