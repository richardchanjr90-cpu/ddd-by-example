using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class ApproveVenuePatchCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
    }
}