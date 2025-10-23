using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{

    public class CreateLoyaltyProgramCommandHandler
        : BaseHandler, ICreateLoyaltyProgramCommandHandler
    {
        public CreateLoyaltyProgramCommandHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProgramCommand request, CancellationToken cancellationToken)
        {
            var program = new LoyaltyProgram
            {
                VenueId = request.VenueId,
                Name = request.Name,
                StartDate = request.StartedDate,
                EndDate = request.EndedDate,
                Description = request.Description,
                IsPublished = request.IsPublished,
                IsArchived = request.IsArchived,
            };

            Context.LoyaltyPrograms.Add(program);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program.Id
            };
        }
    }
}
