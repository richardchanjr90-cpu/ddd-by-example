<<<<<<< Updated upstream
﻿using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
=======
﻿using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
>>>>>>> Stashed changes

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class ArchiveWorkerCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public long Id { get; set; }
    }
}