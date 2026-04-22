using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class ApproveVenuePatchCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
    }
}