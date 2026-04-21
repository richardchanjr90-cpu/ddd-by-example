using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class CreateWorkerCommandHandler
        : IRequestHandler<CreateWorkerCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var phone = accessor.HttpContext.User
                .Claims
                .First(x => x.Type == ClaimTypes.MobilePhone).Value;

            var userId = accessor.HttpContext.User
                .Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var worker = await workerRepository.GetByPhoneAsync(request.Phone, cancellationToken);

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker?.Id
            };

            return commandResult;
        }
    }
}
