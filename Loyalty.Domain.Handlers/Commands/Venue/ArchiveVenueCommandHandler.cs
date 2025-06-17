using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;

namespace Loyalty.Domain.Handlers.Commands.Venue
{
    public class ArchiveVenueCommandHandler : BaseHandler, IArchiveVenueCommandHandler
    {
        public ArchiveVenueCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
