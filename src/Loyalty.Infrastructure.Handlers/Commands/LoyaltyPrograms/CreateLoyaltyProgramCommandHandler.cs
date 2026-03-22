using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class CreateLoyaltyProgramCommandHandler
        : BaseHandler, ICreateLoyaltyProgramCommandHandler
    {
        private readonly IMediator mediator;

        public CreateLoyaltyProgramCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            CreateLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = new LoyaltyProgram
            {
                VenueId = request.VenueId,
                Name = request.Name,
                StartDate = request.StartedDate,
                EndDate = request.EndedDate,
                Description = request.Description
            };

            Context.LoyaltyPrograms.Add(program);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new CreateLoyaltyProgramNotification
                    {
                        Id = program.Id,
                        VenueId = program.VenueId,
                        Name = program.Name,
                        EndDate = program.EndDate,
                        StartDate = program.StartDate,
<<<<<<< Updated upstream
                        IsPublished = program.IsPublished
=======
                        IsPublished = program.IsPublished,
                        Url = program.Url?.ToString()
>>>>>>> Stashed changes
                    },
                    cancellationToken);
            }
            return result;
        }
    }
}