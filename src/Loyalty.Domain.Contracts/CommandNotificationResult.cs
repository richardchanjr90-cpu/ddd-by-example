using System;
using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Contracts
{
    public class CommandNotificationResult : ICommandNotificationResult
    {
        public IList<Func<INotification>> OnSuccessNotifications { get; set; } = new List<Func<INotification>>();

        public IList<Func<INotification>> OnFailNotifications { get; set; } = new List<Func<INotification>>();

        public ICommandResult CommandResult { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
