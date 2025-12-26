using System;
using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Contracts
{
    public class CommandNotificationResult : ICommandNotificationResult
    {
        public IList<Func<INotification>> OnSuccessNotifications { get; set; } = new List<Func<INotification>>();

        public IList<Func<INotification>> OnFailNotifications { get; set; } = new List<Func<INotification>>();

        public ICommandResult CommandResult { get; set; }
    }
}
