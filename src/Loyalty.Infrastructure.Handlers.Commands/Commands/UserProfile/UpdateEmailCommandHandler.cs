using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.UserProfile
{
    public class UpdateEmailCommandHandler
        : IRequestHandler<UpdateEmailCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public UpdateEmailCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetByUidAsync(request.WorkerId, cancellationToken);
            var email = accessor.HttpContext.User.GetEmailOrNull();
            if (!String.IsNullOrEmpty(request.Email) && request.IsEmailVerified && request.Email.Equals(email))
            {
                worker.SetEmail(request.Email);
            }
            else
            {
                throw new LoyaltyValidationException("Email not verified or not valid.", ErrorCode.INVALID_CLAIMS);
            }

            workerRepository.Update(worker);

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };

            return commandResult;
        }
    }
}