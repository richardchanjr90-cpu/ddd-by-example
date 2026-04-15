using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Commands.User
{
    public class AddUserToVenueFirebaseCommand : INotification
    {
        public string UserId { get; set; }

        public List<string> VenueIds { get; set; } = new List<string>();
    }
}
