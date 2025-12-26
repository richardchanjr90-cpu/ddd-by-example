using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class PatchWorkerPhotoCommand : IRequest<ICommandResult>
    {
        public string PhotoUri { get; set; }

        public long Id { get; set; }
    }
}