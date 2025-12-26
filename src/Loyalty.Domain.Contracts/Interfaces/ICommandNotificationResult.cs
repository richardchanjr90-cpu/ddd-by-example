using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Loyalty.Domain.Contracts.Interfaces
{
    public interface ICommandNotificationResult
    {
        IList<Func<INotification>> OnSuccessNotifications { get; set; }

        IList<Func<INotification>> OnFailNotifications { get; set; }

        ICommandResult CommandResult { get; set; }
    }
}
