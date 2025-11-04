using System;
using System.Linq;
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
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class UpdateLoyaltyProgramCommandHandler
        : BaseHandler, IUpdateLoyaltyProgramCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateLoyaltyProgramCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateLoyaltyProgramCommand request, CancellationToken cancellationToken)
        {
            var program = await Context.LoyaltyPrograms
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (program == null)
            {
                program = new LoyaltyProgram
                {
                    Id = request.Id,
                    VenueId = request.VenueId,
                    Name = request.Name,
                    StartDate = request.StartedDate,
                    EndDate = request.EndedDate,
                    Description = request.Description,
                    IsPublished = request.IsPublished,
                    IsArchived = request.IsArchived,
                };

                Context.LoyaltyPrograms.Add(program);
            }
            else
            {
                program.Name = request.Name;
                program.StartDate = request.StartedDate;
                program.EndDate = request.EndedDate;
                program.Description = request.Description;
                program.IsPublished = request.IsPublished;
                program.IsArchived = request.IsArchived;
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new UpdateLoyaltyProgramNotification
                    {
                        Id = program.Id,
                        Name = program.Name,
                        EndDate = program.EndDate,
                        StartDate = program.StartDate
                    }, cancellationToken);
            }
            return result;
        }
    }
}
