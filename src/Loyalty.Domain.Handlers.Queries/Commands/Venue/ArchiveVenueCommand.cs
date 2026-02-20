using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class ArchiveVenueCommand : IRequest<ICommandResult>
    {
        public string OwnerId { get; set; }

        public long Id { get; set; }
    }
}