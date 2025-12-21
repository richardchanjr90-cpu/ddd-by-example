using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Loyalty.Domain.Contracts.Interfaces
{
    public class Notification
    {
        public Type NotificationType { get; set; }

        public INotification Message { get; set; }
    }
}
