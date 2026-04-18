using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.UserProfile
{
    public class UpdateEmailCommandHandler
        : IRequestHandler<UpdateEmailCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public UpdateEmailCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetByUidAsync(request.WorkerId, cancellationToken);

            //if (!request.Email.Equals(worker.Email))
            //{
            //    var emailUser = await workerRepository.GetByPhoneAsync(request.Email, cancellationToken);

            //    if (emailUser != null)
            //    {
            //        throw new LoyaltyValidationException("This email is already taken.", ErrorCode.EMAIL_EXISTS);
            //    }
            //}
            worker.SetEmail(request.Email);

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