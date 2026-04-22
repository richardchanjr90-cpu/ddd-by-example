using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class PatchWorkerPhotoCommand : IRequest<ICommandResult>
    {
        public string PhotoUri { get; set; }

        public string UserId { get; set; }
    }
}