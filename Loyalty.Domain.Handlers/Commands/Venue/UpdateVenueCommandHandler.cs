using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;

namespace Loyalty.Domain.Handlers.Commands.Venue
{
    public class UpdateVenueCommandHandler : BaseHandler, IUpdateVenueCommandHandler
    {
        public UpdateVenueCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
