using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Orders
{
    public class PatchOrderCommand : IRequest<ICommandResult>
    {
        public long VenueId { get; set; }

        public long OrderId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
