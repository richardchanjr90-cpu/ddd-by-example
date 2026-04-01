using System;
using LoyaltyClient.Domain.Handlers.Notifications.Base;

namespace LoyaltyClient.Domain.Handlers.Notifications.Code
{
    public class CodeGeneratedNotification : IClientVenueNotification
    {
        public string UserId { get; set; }

        public string CodeValue { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
