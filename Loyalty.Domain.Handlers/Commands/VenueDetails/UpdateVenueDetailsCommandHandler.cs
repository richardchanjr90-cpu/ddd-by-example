using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Domain.Handlers.Commands.VenueDetails
{
    public class UpdateVenueDetailsCommandHandler : BaseHandler, IUpdateVenueDetailsCommandHandler
    {
        public UpdateVenueDetailsCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(UpdateVenueDetailsCommand request, CancellationToken cancellationToken)
        {
            var details = await Context.VenueDetails
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (details == null)
            {
                details = request.ToSingle();
                Context.VenueDetails.Add(details);
            }
            else
            {
                details.FullDescription = request.FullDescription;
                details.Phones = String.Join(",", request.Phones); 
                details.PhotosUrl = String.Join(",", request.PhotosUrl);
                details.WebSites = String.Join(",", request.WebSites);
                details.WorkingHours = request.WorkingHours;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = details.Id        
            };
        }
    }
}
