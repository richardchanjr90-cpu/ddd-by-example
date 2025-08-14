using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, ICreateVenueCommandHandler
    {
        public CreateVenueCommandHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            return new CommandResult
            {
                Success = true
            };
        }
    }
}