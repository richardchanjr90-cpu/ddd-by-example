using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramNotification : INotification
    {
        public long Id { get; set; }
    }
}
