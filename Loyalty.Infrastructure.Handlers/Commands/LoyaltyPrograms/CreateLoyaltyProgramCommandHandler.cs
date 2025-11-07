using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class CreateLoyaltyProgramCommandHandler
        : BaseHandler, ICreateLoyaltyProgramCommandHandler
    {
        private readonly IMediator mediator;

        public CreateLoyaltyProgramCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = new LoyaltyProgram
            {
                VenueId = request.VenueId,
                Name = request.Name,
                StartDate = request.StartedDate,
                EndDate = request.EndedDate,
                Description = request.Description,
                IsPublished = request.IsPublished,
                IsArchived = request.IsArchived
            };

            Context.LoyaltyPrograms.Add(program);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program.Id
            };

            if (result.Success)
                await mediator.Publish(
                    new CreateLoyaltyProgramNotification
                    {
                        Id = program.Id,
                        VenueId = program.VenueId,
                        Name = program.Name,
                        EndDate = program.EndDate,
                        StartDate = program.StartDate
                    },
                    cancellationToken);
            return result;
        }
    }
}