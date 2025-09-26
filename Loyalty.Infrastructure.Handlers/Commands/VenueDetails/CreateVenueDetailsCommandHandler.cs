using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Infrastructure.Handlers.Extensions;

namespace Loyalty.Infrastructure.Handlers.Commands.VenueDetails
{
    public class CreateVenueDetailsCommandHandler : BaseHandler, ICreateVenueDetailsCommandHandler
    {
        public CreateVenueDetailsCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateVenueDetailsCommand request, CancellationToken cancellationToken)
        {
            var details = request.ToSingle();
            Context.VenueDetails.Add(details);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = details.Id
            };
        }
    }
}