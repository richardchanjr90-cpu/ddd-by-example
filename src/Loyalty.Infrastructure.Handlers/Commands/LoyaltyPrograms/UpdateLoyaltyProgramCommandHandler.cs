using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class UpdateLoyaltyProgramCommandHandler
        : BaseHandler, IUpdateLoyaltyProgramCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateLoyaltyProgramCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            UpdateLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = await Context.LoyaltyPrograms
                .Include(x => x.LoyaltyProductGroups)
                .ThenInclude(x => x.Group)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (program == null)
            {
                throw new LoyaltyValidationException("Does not exist.", ErrorCode.INCORRECT_LOYALTY_PROGRAM);
            }
            else
            {
                if (program.IsPublished)
                {
                    throw new LoyaltyValidationException("You can't change already published program.", ErrorCode.IS_PUBLISHED);
                }

                program.Name = request.Name;
                program.StartDate = request.StartedDate;
                program.EndDate = request.EndedDate;
                program.Description = request.Description;

                if (request.IsPublished && program.LoyaltyProductGroups?.Count == 0)
                {
                    throw new LoyaltyValidationException("You can't publish group without any LoyaltyProductGroups attached.", ErrorCode.FAILED_TO_PUBLISH);
                }
                program.IsPublished = request.IsPublished;
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
                        Url = program.Url?.ToString(),
                        EndDate = program.EndDate,
                        StartDate = program.StartDate,
                        IsPublished = program.IsPublished,
                        Description = program.Description
                    }, cancellationToken);
            }
            return result;
        }
    }
}